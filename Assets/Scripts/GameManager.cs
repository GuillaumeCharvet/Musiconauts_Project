using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum miniGame
{
    none, simonSays, equalizer//, sinusGame
}

public enum colorTint
{
    red, green, blue, yellow, purple, orange, white
}

public class GameManager : MonoBehaviour
{
    //VAR ETIENNE----------------------------

    public miniGame currentMiniGame;                        //Mini-jeu actuel. Si vide alors un mini-jeu va être choisir aléatoirement
    public miniGame lastMiniGame;                           //Dernier mini-jeu sélectionné. Empêche d'avoir le même deux fois d'affilé
    public miniGame[] allMiniGames;                         //Array à remplir dans l'inspector. Ce sont les prefabs des mini-jeux

    public colorTint currentColorTint = colorTint.white;    //Teinte de couleur actuelle
    public colorTint targetColorTint;                       //Teinte de couleur vers laquelle on va se diriger
    public Color[] vectorMatchingTints;                     //Les différentes couleurs pour le changement de Tints du niveau (A remplir dans l'inspector)
    public Vector4 globalColorTint;                         //Vector 4 de la couleur actuelle. Ses valeurs peuvent être lerpées vers la celle de la couleur cible

    public MiniGameSpawner mgSpawner;                       //Ref au script de spawn de mini-jeux
    public ChangeColorTint[] cct;                           //Scripts pour changer la couleur du niveau entier
    public SimonSays simonsays;                             //Ref (pour référence) au SimonSays
    public publicMovements[] foules;                        //Ref (pour référence) aux publics
    public TextMeshProUGUI tm;                              //TextMesh du haut, pour dire ce qu'il faut faire
    public TextMeshProUGUI tmGG;                            //TextMesh qui dit Well Done ou autre
    private GameObject spawnedMiniGame;

    [Range(0f, 1f)]
    public float enjaillement = 0;                          //Niveau d'enjaillement allant de 0 à 1

    public float enjaDecreFactor = 0.03f;                   //Facteur de diminution de l'enjaillement (à augmenter selon les niveaux)
    public int nvDifficulte = 1;                            //Niveau de difficulté allant de 1 à 3
    public bool transition = false;                         //Bool pour savoir si la transition est en cours

    //FONCTIONS ------------------------------------

    private void Awake()
    {
        mgSpawner = FindObjectOfType<MiniGameSpawner>();
        cct = FindObjectsOfType<ChangeColorTint>();                     //Assigne les variables aux objects de la scène
        TextMeshProUGUI[] _tms = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI _tm in _tms)
        {
            if (_tm.name == "MiniJeuText")
            {
                tm = _tm;
            }
            else if (_tm.name == "MiniJeuTextGG")
            {
                tmGG = _tm;
            }
        }
        foules = FindObjectsOfType<publicMovements>();

        RandomColorTint();                                              //Couleur aléatoire pour le début de la partie
        LerpToColor(targetColorTint, 0);
    }

    private void Update()
    {
        NiveauDifficulteChanger();                                      //Change nvDifficulte en fonction de enjaillement
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
    }

    public void RandomColorTint()
    {
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(colorTint)).Length);
        targetColorTint = (colorTint)randomIndex;
    }

    public void LerpToColor(colorTint colorTarget, float timer)
    {
        if (timer == 0)
        {
            int indexEnum = (int)colorTarget;
            globalColorTint = vectorMatchingTints[indexEnum];
        }
    }

    private void PlaySimonSays()
    {
        spawnedMiniGame = mgSpawner.SpawnSimonSays();
    }

    private void PlaySinusGame()
    {
        spawnedMiniGame = mgSpawner.SpawnSinusGame();
    }

    private void PlayEqualizer()
    {
        spawnedMiniGame = mgSpawner.SpawnEqualizer();
    }

    public IEnumerator Transition()
    {
        transition = true;

        AssignationMiniGame();

        if (currentMiniGame == miniGame.none)
        {
            Debug.LogWarning("CURRENT MINI GAME VIDE");
        }

        Debug.Log("last mini game : " + lastMiniGame + " current mini game : " + currentMiniGame);

        if (enjaillement > 0)
        {
            yield return new WaitForSeconds(0.2f);

            tmGG.text = "WELL DONE !";

            RandomColorTint();
            LerpToColor(targetColorTint, 0);
        }

        switch (nvDifficulte)
        {
            case 1:
                yield return new WaitForSeconds(0.9f);
                break;

            case 2:
                yield return new WaitForSeconds(0.6f);
                break;

            case 3:
                yield return new WaitForSeconds(0.4f);
                break;
        }

        if (enjaillement > 0)
        {
            tmGG.text = "";

            yield return new WaitForSeconds(0.2f);
        }

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
            nvDifficulte = 2;
        }
        else
        {
            nvDifficulte = 3;
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
        for (int i = 0; i < foules.Length; i++)
        {
            foules[i].coupDeFolie = true;
        }
    }
}