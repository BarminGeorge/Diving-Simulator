using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffect : MonoBehaviour
{
    public ParticleSystem Splash;

    public void ShowSplash(Transform transform, float angle)
    {
        Splash.transform.position = new Vector2(transform.position.x, Constants.LevelWater);
        ChooseSplash(angle);
        Splash.Play();
        StartCoroutine(WaitSplashTime(Constants.TimeSplash));
    }

    private void ChooseSplash(float angle)
    {
        var normalizedAngle = angle / 90f;
        
        var scaleX = Mathf.Lerp(Constants.MinSplashScaleX, Constants.MaxSplashScaleX, normalizedAngle);
        var scaleY = Mathf.Lerp(Constants.MinSplashScaleY, Constants.MaxSplashScaleY, normalizedAngle);
        var lifeTime = Mathf.Lerp(Constants.MinSplashLifeTime, Constants.MaxSplashLifeTime, normalizedAngle);
        
        var mainModule = Splash.main;
        mainModule.startLifetime = lifeTime;
        Splash.transform.localScale = new Vector3(scaleX, scaleY, Splash.transform.localScale.z);
    }
    
    private IEnumerator WaitSplashTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Splash.Stop();
    }
}