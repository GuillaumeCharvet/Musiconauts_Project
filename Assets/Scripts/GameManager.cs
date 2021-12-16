using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //VAR ETIENNE----------------------------
    public string currentMiniGame = "";
    public string[] allMiniGames;
    public TextMeshProUGUI tm;
    public GameObject[] objectsADesactiver;
    [Range(0f, 1f)]
    public float enjaillement = 0;
    public int nvDifficulte = 1;
    public SimonSays simonsays;

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
        TextMeshProUGUI[] _tms = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI _tm in _tms)
        {
            if(_tm.name == "MiniJeuText")
            {
                tm = _tm;
            }
        }

        foreach (GameObject go in objectsADesactiver)
        {
            go.SetActive(false);
        }

        curseurs = FindObjectsOfType<Cursormove>();
        resetRectangles = FindObjectsOfType<PlacementWinZone>();
    }

    private void Update()
    {
        if (currentMiniGame == "")
        {
            foreach (GameObject go in objectsADesactiver)
            {
                if (go.activeSelf)
                {
                    go.SetActive(false);
                }
            }

            int random = Random.Range(0, allMiniGames.Length);
            currentMiniGame = allMiniGames[random];

            //FORCE A PRENDRE LE SIMON
            currentMiniGame = "Equalizer";
            //FORCE A PRENDRE LE SIMON

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
                    Debug.LogWarning("GAMEMANAGER INCORRECT MINIGAME NAME");
                    break;
            }
        }

        else if (currentMiniGame == "Equalizer")
        {
            interogeReboot = false;                     //COCO
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
        foreach (GameObject go in objectsADesactiver)
        {
            if (go.transform.name == "SINUSGAME")
            {
                go.SetActive(true);
            }
        }

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

        Debug.Log("On lance l'equalizer");
    }

    //FONCTIONS CORENTIN----------------------

    public void WinCount()
    {
        winCount++;
        if (winCount >= 3)
        {
            currentMiniGame = "";
            Lose();

            for(int i = 0; i < resetRectangles.Length; i++)
            {
                resetRectangles[i].Reset();
            }
        }
    }

    public void Lose()
    {
        Debug.Log("Reboot");
        winCount = 0;
        for (var i = 0; i < curseurs.Length; i++)
        {
            curseurs[i].cursorStop = false;
        }
    }

}
