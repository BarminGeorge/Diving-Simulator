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
        if (angle <= 2)
        {
            ResultMark = 10;
            return;
        }
        
        ResultMark = Mathf.Round((10 - (angle / 90) * 10 + Random.Range(-0.5f, 0.5f)) * 2) / 2;
        if (ResultMark < 0) ResultMark = 0;
        if (ResultMark >10) ResultMark = 10;
    }

    private void ShowMark()
    {
        if (ResultMark != -1 && !IsMarkShow)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 10);
            IsMarkShow = true;
        }
    }
}
