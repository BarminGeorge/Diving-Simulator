using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public Score score;
    public Competitors competitors;
    public TextMeshProUGUI[] leaderboardTexts;
    public bool LeaderBoardIsShow = false;

    private void Update()
    {
        if (score.ScoreIsCalculated)
            CreateLeaderBoard();
    }

    private void CreateLeaderBoard()
    {
        var leaderboard = new List<KeyValuePair<string, float>>();
        leaderboard.Add(new KeyValuePair<string, float>("Diver", score.GeneralScore));
        
        foreach (var rival in competitors.Rivals)
            leaderboard.Add(new KeyValuePair<string, float>(rival.Key, rival.Value));
        
        leaderboard.Sort((x, y) => y.Value.CompareTo(x.Value));

        for (var i = 0; i < leaderboardTexts.Length; i++)
        {
            if (i < leaderboard.Count)
                leaderboardTexts[i].text = $"{i + 1}  {leaderboard[i].Key}   {leaderboard[i].Value}";
        }
        ShowLeaderboard();
    }

    private void ShowLeaderboard()
    {
        LeaderBoardIsShow = true;
    }
}