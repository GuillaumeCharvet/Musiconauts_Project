using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    [SerializeField]
    private TextMesh txtMeshScore;

    public int totalScore, victoires, erreurs, defaites;

    private void Start()
    {
        txtMeshScore.text = "0";
    }

    private void Update()
    {
        CheckIfDisplayedScoreIsDifferent();
    }

    private void CheckIfDisplayedScoreIsDifferent()
    {
        int scoreSwritten = int.Parse(txtMeshScore.text);
        if (totalScore != scoreSwritten)
        {
            UpdateScoreText();
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