using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum colorTint
{
    red, green, blue, yellow, purple, orange, white
}


public class GameManager : MonoBehaviour
{
    //VAR ETIENNE----------------------------
    public bool transition = false;
    public float enjaDecreFactor = 0.03f;
    public string currentMiniGame = "";
    public string lastMiniGame;
    public string[] allMiniGames;
    public TextMeshProUGUI tm;
    public TextMeshProUGUI tmGG;
    public GameObject[] objectsADesactiver;
    [Range(0f, 1f)]
    public float enjaillement = 0;
    public int nvDifficulte = 1;
    public SimonSays simonsays;
    public Color[] vectorMatchingTints;
    public colorTint currentColorTint = colorTint.white;
    public colorTint targetColorTint;
    public Vector4 globalColorTint;
    public ChangeColorTint[] cct;
    public publicMovements[] foules;

    //VAR CORENTIN---------------------------
    public Cursormove cm;
    public int winCount = 0;
    public int loseCount = 0;
    public int rebootCount = 0;
    public bool interogeReboot = false;
    public Cursormove[] curseurs;
    public PlacementWinZone[] resetRectangles;

    //FONCTIONS ETIENNE----------------------
    private void Awake()
    {
        cct = FindObjectsOfType<ChangeColorTint>();

        TextMeshProUGUI[] _tms = FindObjectsOfType<TextMeshProUGUI>();

        curseurs = FindObjectsOfType<Cursormove>();
        resetRectangles = FindObjectsOfType<PlacementWinZone>();

        foreach (TextMeshProUGUI _tm in _tms)
        {
            if(_tm.name == "MiniJeuText")
            {
                tm = _tm;
            } else if (_tm.name == "MiniJeuTextGG")
            {
                tmGG = _tm;
            }
        }

        foreach (GameObject go in objectsADesactiver)
        {
            go.SetActive(false);
        }

        RandomColorTint();
        LerpToColor(targetColorTint, 0);

        foules = FindObjectsOfType<publicMovements>();
    }

    private void Update()
    {
        NiveauDifficulteChanger();

        EnjaillementDecrementation();

        if (currentMiniGame == "" && !transition)
        {
            StartCoroutine(Transition());
        }

        else if (currentMiniGame == "Equalizer")
        {
            interogeReboot = false;                     //COCO
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

    private void playSimonSays()
    {
        

        foreach (GameObject go in objectsADesactiver)
        {
            if (go.transform.name == "SIMONSAYS")
            {
                go.SetActive(true);
            }
        }

        simonsays.GameStart();
    }

    private void playSinusGame()
    {
        /*foreach (GameObject go in objectsADesactiver)
        {
            if (go.transform.name == "SINUSGAME")
            {
                go.SetActive(true);
            }
        }*/

        currentMiniGame = "";           //reset
        tm.text = currentMiniGame;

    }

    private void playEqualizer()
    {
        foreach (GameObject go in objectsADesactiver)
        {
            if (go.transform.name == "EQUALIZER")
            {
                go.SetActive(true);
            }
        }
    }

    public IEnumerator Transition()
    {
        transition = true;
        foreach (GameObject go in objectsADesactiver)
        {
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }

        AssignationMiniGame();


        Debug.Log("last mini game : " + lastMiniGame + " current mini game : " + currentMiniGame);        

        if (enjaillement > 0)
        {
            yield return new WaitForSeconds(0.2f);

            tmGG.text = "WELL DONE !";

            RandomColorTint();
            LerpToColor(targetColorTint, 0);
        }  

        yield return new WaitForSeconds(0.5f);

        if (enjaillement > 0)
        {
            yield return new WaitForSeconds(0.2f);

            tmGG.text = "";
        }

        switch (currentMiniGame)
        {
            case "SimonSays":
                playSimonSays();
                break;
            case "SinusGame":
                playSinusGame();
                break;
            case "Equalizer":
                playEqualizer();
                break;
            default:
                Debug.LogWarning("GAMEMANAGER INCORRECT MINIGAME NAME" + currentMiniGame);
                break;
        }
        transition = false;
    }

    public void NiveauDifficulteChanger()
    {
        if(enjaillement < 0.4f)
        {
            nvDifficulte = 1;
        } else if (enjaillement < 0.8f)
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
        /*while (lastMiniGame == currentMiniGame)
        {*/
            //Debug.Log("ITERATION BOUCLE WHILE");
            int random = Random.Range(0, allMiniGames.Length);
            currentMiniGame = allMiniGames[random];
        //}
        lastMiniGame = currentMiniGame;
    }

    public void FouleEnDelire()
    {
        for (int i = 0; i < foules.Length; i++)
        {
            foules[i].coupDeFolie = true;
        }
    }

    //FONCTIONS CORENTIN----------------------

    public void WinCount()
    {
        winCount++;
        if (winCount >= 3)
        {
            enjaillement += 0.1f;
            if (enjaillement >= 1)
            {
                enjaillement = 1;
            }
            FouleEnDelire();

            for (int i = 0; i < resetRectangles.Length; i++)
            {
                resetRectangles[i].Reset();
                curseurs[i].cursorStop = false;
            }

            Lose();

            currentMiniGame = "";


        }
    }

    public void Lose()
    {
        winCount = 0;
        for (var i = 0; i < curseurs.Length; i++)
        {
            curseurs[i].cursorStop = false;
        }
    }

}
