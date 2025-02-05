using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;
    private float fixedPlayerX;
    private bool isXFixed = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    } 

    private void LateUpdate()
    {
        var cameraPosition = transform.position;
        cameraPosition.x = player.position.x;
        cameraPosition.y = Mathf.Max(player.position.y, Constants.CameraMinLevel);
        
        if (cameraPosition.y <= Constants.CameraMinLevel)
        {
            if (!isXFixed)
            {
                fixedPlayerX = cameraPosition.x; 
                isXFixed = true;
            }
            cameraPosition.x = fixedPlayerX;
        }

        transform.position = cameraPosition;
    }
}