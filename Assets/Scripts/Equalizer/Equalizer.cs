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

    private void Update()
    {
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
        int winCount = 0;
        bool lose = false;

        for (int i = 0; i < cursors.Length; i++)
        {
            if (cursors[i].cursorStop)
            {
                if (cursors[i].cursorCollid)
                {
                    winCount++;
                }
                else
                {
                    lose = true;
                }
            }
        }

        if (winCount == 3)
        {
            //GAGNE
        }
        else if (lose)
        {
            //PERDU
        }
    }
}