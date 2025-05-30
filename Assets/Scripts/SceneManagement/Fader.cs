using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Fader : Singleton<Fader>
{
    [SerializeField] Image fadeScreen;
    [SerializeField] float fadeSpeed = 1f;
    IEnumerator fadeRoutine;
    public void FadeToBlack()
    {
        if(fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }
    public void FadeToClear()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }
    IEnumerator FadeRoutine(float targetAlpha)
    {
        while(!Mathf.Approximately(fadeScreen.color.a, targetAlpha))
        {
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,alpha);
            yield return null;
        }
    }
}
