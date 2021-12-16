using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Cursormove cm;

    public int winCount = 0;
    public int loseCount = 0;
    public int rebootCount = 0;
    public bool interogeReboot = false;
    public Cursormove[] curseurs;

    public void Start()
    {
        curseurs = FindObjectsOfType<Cursormove>();

    }

    public void Update()
    {
        interogeReboot = false;
    }
    public void WinCount()
    {
        winCount++;
        if (winCount >= 3)
        {
            Debug.Log("Fin du jeu");
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
