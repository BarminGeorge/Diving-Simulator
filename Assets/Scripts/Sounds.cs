using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public SoundArrays[] soundArrays;
    public AudioSource audioSource;
    public bool SoundPlayed = false;
    public SplashEffect Splash;

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        SoundPlayed = true;
    }

    public void TryChooseSound(Transform transform, Vector2 forwardDirection)
    {
        if (transform.position.y <= Constants.LevelWater && !SoundPlayed)
            ChooseSound(transform, forwardDirection);
    }

    public void ChooseSound(Transform transform, Vector2 direction)
    {
        var angle = Vector2.Angle(direction, Vector2.down);
        if (angle > 90) 
            angle = 180 - angle;
        
        Splash.ShowSplash(transform, angle);
        
        var result = 10 - (angle / 90) * 10;
        var i = Random.Range(0, 3);
        if (result <= 1) PlaySound(soundArrays[0].soundArray[i]);
        else if (result <= 4.5) PlaySound(soundArrays[1].soundArray[i]);
        else if (result <= 5.5) PlaySound(soundArrays[2].soundArray[i]);
        else if (result <= 6.5) PlaySound(soundArrays[3].soundArray[i]);
        else if (result <= 8.5) PlaySound(soundArrays[4].soundArray[i]);
        else if (result <= 9.5) PlaySound(soundArrays[5].soundArray[i]);
        else PlaySound(soundArrays[6].soundArray[i]);
    }

    [System.Serializable]
    public class SoundArrays
    {
        public AudioClip[] soundArray;
    }
}