using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string currentMiniGame = "";

    public string[] allMiniGames;

    public TextMeshProUGUI tm;

    public GameObject[] objectsADesactiver;

    [Range(0f, 1f)]
    public float enjaillement = 0;

    public int nvDifficulte = 1;

    public SimonSays simonsays;



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
            currentMiniGame = "SimonSays";
            //FORCE A PRENDRE LE SIMON

            switch (currentMiniGame)
            {
                case "SimonSays":
                    playSimonSays();
                    break;
                case "SinusGame":
                    playSinusGame();
                    break;
                default:
                    Debug.LogWarning("GAMEMANAGER INCORRECT MINIGAME NAME");
                    break;
            }
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

}
