using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffect : MonoBehaviour
{
    public ParticleSystem Splash;

    public void ShowSplash(Transform transform, Vector2 direction)
    {
        Splash.transform.position = new Vector2(transform.position.x, Constants.LevelWater);
        Splash.Play();
        StartCoroutine(WaitSplashTime(Constants.TimeSplash));
    }

    private IEnumerator WaitSplashTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Splash.Stop();
    }
}