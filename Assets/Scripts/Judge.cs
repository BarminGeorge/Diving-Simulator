using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour
{
    public Dive dive;
    public Score score;
    public float ResultMark = -1;
    public Transform transform;
    public float HeightOfDiver;
    public bool IsMarkShow = false;
    public bool IsMarkGenerated = false;
    public SpriteRenderer spriteRenderer;
    public SpriteCollection Collection;
    private Vector2 Direction;
    
    private void Update()
    {
        HeightOfDiver = dive.transform.position.y;
        Direction = dive.forwardDirection;
        TryGenerateMark();
        ShowMark();
    }

    private void TryGenerateMark()
    {
        if (HeightOfDiver <= Constants.LevelWater && !IsMarkGenerated)
            GenerateMark(Direction);
    }

    private void GenerateMark(Vector2 direction)
    {
        var angle = Vector2.Angle(direction, Vector2.down);
        if (angle >= 90)
            ResultMark = Constants.MinMark;
        if (angle <= Constants.AngleForTenPoints)
            ResultMark = Constants.MaxMark;
        else
        {
            ResultMark = Constants.MaxMark - (angle / 90) * 10 
                         + Random.Range(-Constants.RefereeingError, Constants.RefereeingError);
            ResultMark = Mathf.Round(ResultMark * 2) / 2;
            if (ResultMark < Constants.MinMark) 
                ResultMark = Constants.MinMark;
            if (ResultMark > Constants.MaxMark) 
                ResultMark = Constants.MaxMark;
        }

        IsMarkGenerated = true;
        score.ResultsRefereeing.Add(ResultMark);
        spriteRenderer.sprite = Collection.AllMarksSprites[(int)(ResultMark * 2)];
    }

    private void ShowMark()
    {
        if (ResultMark >= 0 && !IsMarkShow)
            StartCoroutine(ShowMarkCoroutine());
    }
    
    private IEnumerator ShowMarkCoroutine()
    {
        var targetY = transform.position.y + Constants.HeightRiseMark;
        var timeFromStart = 0f;

        var startPosition = transform.position;

        while (timeFromStart < Constants.RiseTimeMark)
        {
            var animationProgress = timeFromStart / Constants.RiseTimeMark;
            transform.position = Vector2.Lerp(startPosition, new Vector2(startPosition.x, targetY), animationProgress);
        
            timeFromStart += Time.deltaTime;
            yield return null;
        }
        
        IsMarkShow = true;
    }
}
