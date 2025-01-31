using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float LimitY = 0;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.y = Mathf.Max(player.position.y, LimitY);
        
        transform.position = temp;
    }
}
