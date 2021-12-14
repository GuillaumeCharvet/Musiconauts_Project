using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string currentMiniGame = "";

    public string[] allMiniGames;

    public TextMeshProUGUI tm;

    [Range(0f, 1f)]
    public float enjaillement = 0;

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
    }

    private void Update()
    {
        if (currentMiniGame == "")
        {
            int random = Random.Range(0, allMiniGames.Length);
            currentMiniGame = allMiniGames[random];


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
        tm.text = currentMiniGame;
    }

    private void playSinusGame()
    {
        tm.text = currentMiniGame;
    }

}
