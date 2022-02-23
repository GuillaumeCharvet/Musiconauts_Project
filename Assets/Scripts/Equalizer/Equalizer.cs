using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equalizer : MonoBehaviour
{
    public GameManager gm;
    public refCursor[] cursors;
    public refWinZone[] winZones;

    [Range(100, 300)]
    public float speed = 100;

    private int curNvDifficulte;
    public int winCount;

    public bool cursorStop = false;
    public bool winCursor = false;
    public bool loseCursor = false;
    public bool rebootcursor = false;
    public bool rebootAffect = false;

    public void Start()
    {
        gm = FindObjectOfType<GameManager>();
        cursors = FindObjectsOfType<refCursor>();
        winZones = FindObjectsOfType<refWinZone>();

        ResetWinZone();

        curNvDifficulte = gm.nvDifficulte;

        switch (curNvDifficulte)
        {
            case 1:
                speed = 100;
                break;

            case 2:
                speed = 170;
                break;

            case 3:
                speed = 240;
                break;
        }
    }

    public void ResetWinZone()
    {
        for (int i = 0; i < winZones.Length; i++)
        {
            float randomVar = Random.Range(-5, 5f);
            winZones[i].transform.localPosition = new Vector3(transform.localPosition.x, randomVar, transform.localPosition.z);
        }
    }

    public void CursorSituation()
    {
        int winCount = 0;                                   //initialise le compte de bons curseurs à 0
        bool lose = false;                                  //victoire par défaut. Au moindre mauvais cursuer, on passe défaite à true

        for (int i = 0; i < cursors.Length; i++)            //pour chaque curseur
        {
            if (cursors[i].cursorStop)                      //vérifie sur le curseur est arrêté
            {
                if (cursors[i].cursorCollid)                //s'il est arrêté et qu'il est dans la zone
                {
                    winCount++;                             //-> on augmente la valeur de compte de bons curseur
                }
                else
                {
                    lose = true;                            //sinon on passe le bool de défaite à true
                }
            }
        }

        if (winCount == 3)                                  //si le nombre de bons curseurs = 3
        {
            StartCoroutine(gm.Win());                       //on gagne
        }
        else if (lose)                                      //sinon
        {
            for (int i = 0; i < cursors.Length; i++)
            {
                cursors[i].cursorStop = false;              //on remet tous les curseurs en marche
            }
            ResetWinZone();                                 //on perd et ça réinitialise les jauges
        }
    }
}