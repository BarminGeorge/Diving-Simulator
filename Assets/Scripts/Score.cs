using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public List<float> ResultsRefereeing;
    public float Coefficient = 2;
    public float GeneralScore;
    public bool ScoreIsCalculated = false;

    private void Update()
    {
        if (ResultsRefereeing.Count == 7 && !ScoreIsCalculated)
            CalculateScore();
    }

    private void CalculateScore()
    {
        ResultsRefereeing.Sort();
        var sum = 0f;
        for (var i = 2; i < 5; i++)
            sum += ResultsRefereeing[i];
        GeneralScore = sum * Coefficient;
        ScoreIsCalculated = true;
    }
}