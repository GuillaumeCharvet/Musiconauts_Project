using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#region Enums

public enum miniGame
{
    none, simonSays, equalizer, duo, spam//, sinusGame
}

public enum colorTint
{
    red, green, blue, yellow, purple, orange, white
}

#endregion Enums

public class GameManager : MonoBehaviour
{
    #region Variables

    [Header("----------Temps des Mini-Jeux----------")]
    public int simonSaysTemps;

    public int equalizerTemps, duoTemps, spamTemps;

    public Level_SO currentLevel;

    public PlayMusic playMusic;

    public miniGame currentMiniGame;                        //Mini-jeu actuel. Si vide alors un mini-jeu va être choisir aléatoirement
    public miniGame lastMiniGame;                           //Dernier mini-jeu sélectionné. Empêche d'avoir le même deux fois d'affilé
    public miniGame[] allMiniGames;                         //Array à remplir dans l'inspector. Ce sont les prefabs des mini-jeux

    private bool isAFailure;

    private TimerGauge timerGauge;

    public colorTint currentColorTint = colorTint.white;    //Teinte de couleur actuelle
    public colorTint targetColorTint;                       //Teinte de couleur vers laquelle on va se diriger
    private Color oldColor;
    private bool oldColorSet;
    private float colorLerpT;
    public Color[] vectorMatchingTints;                     //Les différentes couleurs pour le changement de Tints du niveau (A remplir dans l'inspector)
    public Vector4 globalColorTint;                         //Vector 4 de la couleur actuelle. Ses valeurs peuvent être lerpées vers la celle de la couleur cible

    public MiniGameSpawner mgSpawner;                       //Ref au script de spawn de mini-jeux
    public ChangeColorTint[] cct;                           //Scripts pour changer la couleur du niveau entier
    public publicMovements[] foules;                        //Ref (pour référence) aux publics

    public SpriteRenderer tmGG;                            //TextMesh qui dit Well Done ou autre
    public SpriteRenderer tmTooBad;

    [HideInInspector]
    public int totalScoreSimonSays, totalScoreEQ, totalScoreDuo, totalScoreSpam, SimonSaysReussis, EQreussis, duoReussis, spamReussis;

    [SerializeField]
    private Scoring scoring;

    [SerializeField]
    private AnimTransform animGGOpen, animGGClose, animTooBadOpen, animTooBadClose;

    private GameObject spawnedMiniGame;

    [SerializeField]
    private Animator animatorVictory, animatorFail;

    private Animation animVictory, animFail;

    [Range(0f, 1f)]
    public float enjaillement = 0;                          //Niveau d'enjaillement allant de 0 à 1

    public float enjaDecreFactor = 0.03f;                   //Facteur de diminution de l'enjaillement (à augmenter selon les niveaux)
    public int nvDifficulte = 1;                            //Niveau de difficulté allant de 1 à 3
    public bool transition = false;                         //Bool pour savoir si la transition est en cours

    [HideInInspector]
    public int musicDuration;

    public bool levelHasEnded;

    #endregion Variables

    #region Fonctions Primitives

    private void Awake()
    {
        mgSpawner = FindObjectOfType<MiniGameSpawner>();
        cct = FindObjectsOfType<ChangeColorTint>();                     //Assigne les variables aux objects de la scène
        TextMeshProUGUI[] _tms = FindObjectsOfType<TextMeshProUGUI>();
        timerGauge = FindObjectOfType<TimerGauge>();

        foules = FindObjectsOfType<publicMovements>();

        RandomColorTint();                                              //Couleur aléatoire pour le début de la partie
        LerpToColor(targetColorTint, 0);

        AdaptationLevel();

        musicDuration = (int)currentLevel.music.length + 1;
    }

    private void Update()
    {
        EnjaillementDecrementation();                                   //enjaillement -= Time.deltaTime * facteur

        if (currentMiniGame == miniGame.none && !transition)            //Si aucun mini-jeu n'est sélectionné et la transition n'a pas commencé
        {
            StartCoroutine(Transition());
        }

        if (currentColorTint != targetColorTint)
        {
            bool tousFinis = true;
            for (int i = 0; i < cct.Length; i++)
            {
                if (!cct[i].transitionCouleurFinie)
                {
                    tousFinis = false;
                }
            }
            if (tousFinis)
            {
                currentColorTint = targetColorTint;
                for (int i = 0; i < cct.Length; i++)
                {
                    cct[i].transitionCouleurFinie = false;
                }
            }
        }

        AnimationsManagement();

        if (currentColorTint != targetColorTint)
        {
            LerpToColor(targetColorTint, 1);
        }

        if (levelHasEnded)
        {
            mgSpawner.DestroyMiniGame();
        }
    }

    #endregion Fonctions Primitives

    #region Fonctions Couleurs

    public void RandomColorTint()
    {
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(colorTint)).Length);
        targetColorTint = (colorTint)randomIndex;
    }

    public void LerpToColor(colorTint colorTarget, float timer)
    {
        if (!oldColorSet)
        {
            oldColorSet = true;
            oldColor = globalColorTint;
            colorLerpT = 0;
        }
        if (timer == 0)
        {
            int indexEnum = (int)colorTarget;
            globalColorTint = vectorMatchingTints[indexEnum];
            oldColorSet = false;
            currentColorTint = colorTarget;
        }
        else if (colorLerpT < 1)
        {
            int indexEnum = (int)colorTarget;
            globalColorTint = Color.Lerp(oldColor, vectorMatchingTints[indexEnum], colorLerpT);
            colorLerpT += Time.deltaTime * 15;
        }
        else if (colorLerpT >= 1)
        {
            oldColorSet = false;
            currentColorTint = colorTarget;
        }
    }

    #endregion Fonctions Couleurs

    #region Fonctions Autres

    public IEnumerator Transition()
    {
        transition = true;

        AssignationMiniGame();

        if (enjaillement > 0)
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log(isAFailure);

            if (!isAFailure)
            {
                tmGG.transform.parent.gameObject.SetActive(true);
                animGGOpen.SetCanGo();
            }
        }

        if (isAFailure)
        {
            tmTooBad.transform.parent.gameObject.SetActive(true);
            animTooBadOpen.SetCanGo();
        }

        switch (nvDifficulte)
        {
            case 1:
                yield return new WaitForSeconds(0.7f);
                if (!isAFailure)
                {
                    animGGClose.SetCanGo();
                }
                else
                {
                    animTooBadClose.SetCanGo();
                }
                yield return new WaitForSeconds(0.2f);
                break;

            case 3:
                yield return new WaitForSeconds(0.4f);
                if (!isAFailure)
                {
                    animGGClose.SetCanGo();
                }
                else
                {
                    animTooBadClose.SetCanGo();
                }
                yield return new WaitForSeconds(0.2f);
                break;

            case 6:
                yield return new WaitForSeconds(0.2f);
                if (!isAFailure)
                {
                    animGGClose.SetCanGo();
                }
                else
                {
                    animTooBadClose.SetCanGo();
                }
                yield return new WaitForSeconds(0.2f);
                break;
        }

        if (!isAFailure)
        {
            tmGG.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            tmTooBad.transform.parent.gameObject.SetActive(false);

            isAFailure = false;
        }

        yield return new WaitForSeconds(0.2f);

        switch (currentMiniGame)
        {
            case miniGame.simonSays:
                PlaySimonSays();
                break;

            /*case miniGame.sinusGame:
                PlaySinusGame();
                break;*/

            case miniGame.equalizer:
                PlayEqualizer();
                break;

            case miniGame.duo:
                PlayDuo();
                break;

            case miniGame.spam:
                PlaySpam();
                break;

            default:
                Debug.LogWarning("GAMEMANAGER INCORRECT MINIGAME NAME" + currentMiniGame);
                break;
        }
        transition = false;
    }

    public void NiveauDifficulteChanger()
    {
        if (enjaillement < 0.4f)
        {
            nvDifficulte = 1;
        }
        else if (enjaillement < 0.8f)
        {
            nvDifficulte = 3;
        }
        else
        {
            nvDifficulte = 6;
        }
    }

    public void EnjaillementDecrementation()
    {
        if (enjaillement > 0)
        {
            enjaillement -= enjaDecreFactor * Time.deltaTime;

            if (enjaillement < 0)
            {
                enjaillement = 0;
            }
        }
    }

    public void AssignationMiniGame()
    {
        while (currentMiniGame == lastMiniGame || currentMiniGame == miniGame.none)
        {
            if (lastMiniGame != miniGame.none)
            {
                List<miniGame> miniGamesPossibles;

                miniGamesPossibles = new List<miniGame>();

                for (int i = 1; i < System.Enum.GetValues(typeof(miniGame)).Length; i++)
                {
                    if ((miniGame)i != lastMiniGame)
                    {
                        miniGamesPossibles.Add((miniGame)i);
                    }
                }
                int randomIndex = Random.Range(0, miniGamesPossibles.Count);
                currentMiniGame = miniGamesPossibles[randomIndex];
            }
            else
            {
                int randomIndex = Random.Range(1, System.Enum.GetValues(typeof(miniGame)).Length);
                currentMiniGame = (miniGame)randomIndex;
            }
        }

        lastMiniGame = currentMiniGame;
    }

    public void FouleEnDelire()
    {
        RandomColorTint();
        for (int i = 0; i < foules.Length; i++)
        {
            foules[i].coupDeFolie = true;
        }
    }

    public IEnumerator Win()
    {
        float tempsQuilReste = timerGauge.TimerStop();

        yield return new WaitForSeconds(0.15f);

        scoring.AddToScore(ScoreCalculator(tempsQuilReste));

        switch (currentMiniGame)
        {
            case miniGame.simonSays:
                SimonSaysReussis++;
                totalScoreSimonSays += ScoreCalculator(tempsQuilReste);
                break;

            case miniGame.duo:
                duoReussis++;
                totalScoreDuo += ScoreCalculator(tempsQuilReste);
                break;

            case miniGame.equalizer:
                EQreussis++;
                totalScoreEQ += ScoreCalculator(tempsQuilReste);
                break;

            case miniGame.spam:
                spamReussis++;
                totalScoreSpam += ScoreCalculator(tempsQuilReste);
                break;
        }
        currentMiniGame = miniGame.none;
        enjaillement += 0.1f;
        if (enjaillement >= 1)
        {
            enjaillement = 1;
        }

        NiveauDifficulteChanger();

        FouleEnDelire();
        scoring.victoires++;
        mgSpawner.DestroyMiniGame();
        Debug.Log("bah oui");
    }

    public IEnumerator WinRythm(float value)
    {
        yield return new WaitForSeconds(0.15f);

        scoring.AddToScore(ScoreCalculator(value));

        enjaillement += 0.1f;
        if (enjaillement >= 1)
        {
            enjaillement = 1;
        }

        NiveauDifficulteChanger();

        FouleEnDelire();
        scoring.victoires++;
        mgSpawner.DestroyMiniGame();
        Debug.Log("bah oui");
    }

    private void AnimationsManagement()
    {
        /*if (animatorVictory.gameObject.activeInHierarchy)
        {
            animVictory = animatorVictory.GetComponent<Animation>();
            Debug.Log(animVictory["Victory_anim"].normalizedTime);
            if (animVictory["Victory_anim"].normalizedTime >= 1)
            {
                animVictory["Victory_anim"].normalizedTime = 0;
                animVictory.gameObject.SetActive(false);
            }
        }

        if (animatorFail.gameObject.activeInHierarchy)
        {
            animFail = animatorFail.GetComponent<Animation>();
            if (animFail["Fail_anim"].normalizedTime >= 1)
            {
                animFail["Fail_anim"].normalizedTime = 0;
                animFail.gameObject.SetActive(false);
            }
        }*/
    }

    private void AdaptationLevel()
    {
        playMusic.audioSource.clip = currentLevel.music;
        enjaDecreFactor = currentLevel.enjaillementDecrement;
    }

    private int ScoreCalculator(float tempsQuilReste)
    {
        float score = 0f;

        float baseScoreByMiniGame = 0f;

        switch (currentMiniGame)
        {
            case miniGame.simonSays:
                baseScoreByMiniGame = 1f;
                break;

            case miniGame.equalizer:
                baseScoreByMiniGame = 0.85f;
                break;

            case miniGame.duo:
                baseScoreByMiniGame = 0.7f;
                break;

            case miniGame.spam:
                baseScoreByMiniGame = 0.8f;
                break;

            default:
                Debug.LogError("GameManager.ScoreCalculator - MiniGame non compris dans le switch - " + currentMiniGame.ToString());
                break;
        }

        score = (1 + tempsQuilReste) * baseScoreByMiniGame * nvDifficulte * (1 + enjaillement) * 100;

        return (int)score;
    }

    public void EndMiniGame()
    {
        mgSpawner.DestroyMiniGame();

        scoring.AddToScore((int)(nvDifficulte * (1 + enjaillement) * -5));

        enjaillement -= .1f;
        if (enjaillement < 0)
        {
            enjaillement = 0;
        }

        scoring.defaites++;

        isAFailure = true;
        NiveauDifficulteChanger();

        currentMiniGame = miniGame.none;
    }

    #endregion Fonctions Autres

    #region Fonctions Play

    private void PlaySimonSays()
    {
        spawnedMiniGame = mgSpawner.SpawnSimonSays();
        timerGauge.TimerStart();
    }

    private void PlaySinusGame()
    {
        spawnedMiniGame = mgSpawner.SpawnSinusGame();
        timerGauge.TimerStart();
    }

    private void PlayEqualizer()
    {
        spawnedMiniGame = mgSpawner.SpawnEqualizer();
        timerGauge.TimerStart();
    }

    private void PlayDuo()
    {
        spawnedMiniGame = mgSpawner.SpawnDuo();
        timerGauge.TimerStart();
    }

    private void PlaySpam()
    {
        spawnedMiniGame = mgSpawner.SpawnSpam();
        timerGauge.TimerStart();
    }

    #endregion Fonctions Play

    #region Fonctions EndLevel

    public void EndLvl()
    {
        levelHasEnded = true;
        playMusic.audioSource.Pause();

        timerGauge.TimerStop();
    }

    #endregion Fonctions EndLevel
}