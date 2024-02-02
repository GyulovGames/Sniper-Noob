using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class experemantal : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float duration = 1f; // Duration in seconds
    public float targetAlpha = 0f;

    private float startAlpha;
    private float currentTime;

    public void Start()
    {
        startAlpha = canvasGroup.alpha;
        currentTime = 0f;
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, currentTime / duration);
            canvasGroup.alpha = newAlpha;
            yield return null;
        }
    }
}
