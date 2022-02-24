using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtMeshScore, txtMeshScoreDetails;

    public int totalScore, victoires, erreurs, defaites;

    private GameManager gm;

    [SerializeField]
    private SpriteRenderer rectangle;

    private int moyenneSimonSays, moyenneEQ, moyenneDuo;

    private bool isWritten;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        txtMeshScore.text = "";
        txtMeshScoreDetails.text = "";
    }

    private void Update()
    {
        if (gm.levelHasEnded && !isWritten)
        {
            rectangle.gameObject.SetActive(true);

            if (gm.SimonSaysReussis == 0)
            {
                moyenneSimonSays = 0;
            }
            else
            {
                moyenneSimonSays = gm.totalScoreSimonSays / gm.SimonSaysReussis;
            }

            if (gm.EQreussis == 0)
            {
                moyenneEQ = 0;
            }
            else
            {
                moyenneEQ = gm.totalScoreEQ / gm.EQreussis;
            }

            if (gm.duoReussis == 0)
            {
                moyenneDuo = 0;
            }
            else
            {
                moyenneDuo = gm.totalScoreDuo / gm.duoReussis;
            }

            txtMeshScore.text = totalScore.ToString();
            txtMeshScoreDetails.text = "Victories : " + victoires + "\nFails : " + defaites;
            txtMeshScoreDetails.text += "\n\nMoyenne score SimonSays : " + moyenneSimonSays;
            txtMeshScoreDetails.text += "\nMoyenne score EQ : " + moyenneEQ;
            txtMeshScoreDetails.text += "\nMoyenne score Duo : " + moyenneDuo;
            isWritten = true;
        }
    }

    private void UpdateScoreText()
    {
        txtMeshScore.text = totalScore.ToString();
    }

    public void AddToScore(int value)
    {
        totalScore += value;

        if (totalScore < 0)
        {
            totalScore = 0;
        }
    }
}