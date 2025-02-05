using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentRound {get; private set;} = 0;
    public Dive diver;
    public Judge[] Judges;
    public Competitors competitors;
    public Score score;
    public Leaderboard Leaderboard;
    private int totalRounds = CompetitionProgram.Program.Length;
    private Vector2[] judgeStartPositions;

    private void Awake()
    {
        judgeStartPositions = new Vector2[]
        {
            new Vector2(Constants.MarkX1, Constants.MarkY),
            new Vector2(Constants.MarkX1 + 2, Constants.MarkY),
            new Vector2(Constants.MarkX1 + 4, Constants.MarkY),
            new Vector2(Constants.MarkX1 + 6, Constants.MarkY),
            new Vector2(Constants.MarkX1 + 8, Constants.MarkY),
            new Vector2(Constants.MarkX1 + 10, Constants.MarkY),
            new Vector2(Constants.MarkX1 + 12, Constants.MarkY)
        };
        competitors.AddPointsToRivals();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && currentRound < totalRounds - 1 && Leaderboard.LeaderBoardIsShow)
            StartNewRound();
    }

    private void StartNewRound()
    {
        currentRound++;
        ResetGameState();
        competitors.AddPointsToRivals();
    }

    private void ResetGameState()
    {
        ResetPlayer();
        ResetJudges();
        ResetScore();
        Leaderboard.LeaderBoardIsShow = false;
    }

    private void ResetPlayer()
    {
        diver.transform.position = new Vector2(Constants.DiverX, Constants.DiverY);
        diver.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        var dive = diver.GetComponent<Dive>();
        if (dive != null)
        {
            dive.canJump = true;
            dive.classOfDive = new ClassOfDive(DiveType.None);
            dive.SoundPlayed = false;
        }
    }

    private void ResetJudges()
    {
        for (var i = 0; i < Judges.Length; i++)
        {
            Judges[i].enabled = true;
            Judges[i].transform.position = judgeStartPositions[i];
            Judges[i].ResultMark = -1;
            Judges[i].IsMarkShow = false;
            Judges[i].IsMarkGenerated = false;
        }
    }

    private void ResetScore()
    {
        score.ScoreIsCalculated = false;
        score.ResultsRefereeing.Clear();
        score.UpdateDive();
    }
}
