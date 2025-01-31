using System.Collections.Generic;
using UnityEngine;

public class Competitors : MonoBehaviour
{
    public Dictionary<string, float> Rivals = new Dictionary<string, float>
    {
        { "name1", 0 },
        { "name2", 0 },
        { "name3", 0 },
        { "name4", 0 },
        { "name5", 0 },
        { "name6", 0 },
        { "name7", 0 },
        { "name8", 0 },
        { "name9", 0 },
        { "name10", 0 },
        { "name11", 0 }
    };

    public void AddPointsToRivals()
    {
        var newScores = new Dictionary<string, float>();
        
        foreach (var rival in Rivals.Keys)
            newScores[rival] = CreateNewScoreForRival();
        
        foreach (var rival in newScores)
            Rivals[rival.Key] += rival.Value;
    }

    private float CreateNewScoreForRival()
    {  
        var score = Random.Range(80f, 100f);
        return Mathf.Round(score * 100f) / 100f; // Округление до 2 знаков после запятой
    }
}