using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public List<float> ResultsRefereeing;
    public GameManager gameManager;
    public string Dive;
    public float GeneralScore;
    public Transform transform;
    public Judge LastJudge;
    public bool ScoreIsCalculated = false;
    
    private void Awake()
    {
        UpdateDive();
    }

    public void UpdateDive()
    {
        if (gameManager != null)
            Dive = CompetitionProgram.Program[gameManager.currentRound];
    }

    private void Update()
    {
        if (ResultsRefereeing.Count == 7 && !ScoreIsCalculated && LastJudge.IsMarkShow)
            CalculateScore();
    }

    private void CalculateScore()
    {
        ResultsRefereeing.Sort();
        var sum = 0f;
        for (var i = 2; i < 5; i++)
            sum += ResultsRefereeing[i];
        GeneralScore += sum * JumpDifficulty.Platform10M[Dive];
        ScoreIsCalculated = true;
    }
}