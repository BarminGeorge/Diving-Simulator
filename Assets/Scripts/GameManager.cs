using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentRound = 0;
    public Dive diver;
    public Mark mark1;
    public Mark mark2;
    public Mark mark3;
    public Mark mark4;
    public Mark mark5;
    public Mark mark6;
    public Mark mark7;
    public Score score;
    private int totalRounds = CompetitionProgram.Program.Length;
    
    private Vector2[] markStartPositions;

    private void Awake()
    {
        markStartPositions = new Vector2[]
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
        if (Input.GetKeyDown(KeyCode.Return) && currentRound < totalRounds)
        {
            StartNewRound();
        }
    }

    private void StartNewRound()
    {
        currentRound++;
        ResetGameState();
    }

    private void ResetGameState()
    {
        ResetPlayer();
        ResetMarks();
        score.ScoreIsCalculated = false;
        score.ResultsRefereeing.Clear();
        score.UpdateDive();
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

    private void ResetMarks()
    {
        Mark[] marks = { mark1, mark2, mark3, mark4, mark5, mark6, mark7 };
        for (int i = 0; i < marks.Length; i++)
        {
            marks[i].transform.position = markStartPositions[i];
            marks[i].enabled = true;
            marks[i].ResultMark = -1;
            marks[i].IsMarkShow = false;
            marks[i].IsMarkGenerated = false;
        }
    }
}
