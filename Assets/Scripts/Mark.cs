using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public Dive dive;
    public float ResultMark = -1;
    public Transform transform;
    public float HeightOfDiver;
    private Vector2 Direction;
    private bool IsMarkGenerated = false;
    private bool IsMarkShow = false;
    
    private void Update()
    {
        HeightOfDiver = dive.transform.position.y;
        Direction = dive.forwardDirection;
        TryGenerateMark();
        ShowMark();
    }

    private void TryGenerateMark()
    {
        if (HeightOfDiver <= 0 && !IsMarkGenerated)
            GenerateMark(Direction);
    }

    private void GenerateMark(Vector2 direction)
    {
        IsMarkGenerated = true;
        var angle = Vector2.Angle(direction, Vector2.down);
        if (angle >= 90)
            ResultMark = 0;
        if (angle <= Constants.AngleForTenPoints)
        {
            ResultMark = 10;
            return;
        }

        ResultMark = 10 - (angle / 90) * 10 + Random.Range(-Constants.RefereeingError, Constants.RefereeingError);
        ResultMark = Mathf.Round(ResultMark * 2) / 2;
        if (ResultMark < 0) ResultMark = 0;
        if (ResultMark > 10) ResultMark = 10;
    }

    private void ShowMark()
    {
        if (ResultMark >= 0 && !IsMarkShow)
            StartCoroutine(ShowMarkCoroutine());
    }
    
    private IEnumerator ShowMarkCoroutine()
    {
        var targetY = transform.position.y + Constants.HeightRiseMark;
        var duration = Constants.RiseTimeMark;
        var timeFromStart = 0f;

        Vector2 startPosition = transform.position;

        while (timeFromStart < duration)
        {
            var animationProgress = timeFromStart / duration;
            transform.position = Vector2.Lerp(startPosition, new Vector2(startPosition.x, targetY), animationProgress);
        
            timeFromStart += Time.deltaTime;
            yield return null;
        }
        
        IsMarkShow = true;
    }
}
