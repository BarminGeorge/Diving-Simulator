using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentRound {get; private set;} = 0;
    public Dive diver;
    public Judge judge1;
    public Judge judge2;
    public Judge judge3;
    public Judge judge4;
    public Judge judge5;
    public Judge judge6;
    public Judge judge7;
    public Score score;
    private int totalRounds = CompetitionProgram.Program.Length;
    private Vector2[] judgeStartPositions;

    private void Awake()
    {
        judgeStartPositions = new Vector2[]
        {
            new Vector2(0, Constants.MarkY),
            new Vector2(2, Constants.MarkY),
            new Vector2(4, Constants.MarkY),
            new Vector2(6, Constants.MarkY),
            new Vector2(8, Constants.MarkY),
            new Vector2(10, Constants.MarkY),
            new Vector2(12, Constants.MarkY)
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && currentRound < totalRounds && score.ScoreIsCalculated)
            StartNewRound();
    }

    private void StartNewRound()
    {
        currentRound++;
        ResetGameState();
    }

    private void ResetGameState()
    {
        ResetPlayer();
        ResetJudges();
        ResetScore();
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
        Judge[] judges = { judge1, judge2, judge3, judge4, judge5, judge6, judge7 };
        for (var i = 0; i < judges.Length; i++)
        {
            judges[i].enabled = true;
            judges[i].transform.position = judgeStartPositions[i];
            judges[i].ResultMark = -1;
            judges[i].IsMarkShow = false;
            judges[i].IsMarkGenerated = false;
        }
    }

    private void ResetScore()
    {
        score.ScoreIsCalculated = false;
        score.ResultsRefereeing.Clear();
        score.UpdateDive();
        score.transform.position = new Vector2(transform.position.x + 10, transform.position.y);
    }
}
