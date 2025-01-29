using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public List<float> ResultsRefereeing;
    public string Dive = CompetitionProgram.Program[0];
    public float GeneralScore;
    public Transform transform;
    public Mark mark;
    
    private bool ScoreIsCalculated = false;

    private void Update()
    {
        if (ResultsRefereeing.Count == 7 && !ScoreIsCalculated && mark.IsMarkShow)
            CalculateScore();
    }

    private void CalculateScore()
    {
        ResultsRefereeing.Sort();
        var sum = 0f;
        for (var i = 2; i < 5; i++)
            sum += ResultsRefereeing[i];
        GeneralScore = sum * JumpDifficulty.Platform10M[Dive];
        ScoreIsCalculated = true;
        transform.position = new Vector2(transform.position.x - 10, transform.position.y);
    }
}