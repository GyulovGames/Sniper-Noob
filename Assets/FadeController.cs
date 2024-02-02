using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] private float fadeDuration;
    [SerializeField] private float targetAlpha;

    private float startAlpha;
    private float appearCurrentTime;
    private float disappearCurrentTime;
 

    public void Appear(CanvasGroup canvasGrp)
    {
        if (canvasGrp.alpha == 0)
        {
            canvasGrp.gameObject.SetActive(true);
        }

        StartCoroutine(AppearCoroutine(canvasGrp));
        appearCurrentTime = 0f;
    }

    private IEnumerator AppearCoroutine(CanvasGroup canvasGroup)
    {
        while(appearCurrentTime < fadeDuration)
        {
            appearCurrentTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0, 1, appearCurrentTime / fadeDuration);
            canvasGroup.alpha = newAlpha;

            if(canvasGroup.alpha == 1)
            {
                StopCoroutine(AppearCoroutine(canvasGroup));
            }

            yield return null;
        }
    }




    public void Desappear(CanvasGroup canvasGrp)
    {
        StartCoroutine(DisappearCoroutine(canvasGrp));
        disappearCurrentTime = 0f;
    }

    private IEnumerator DisappearCoroutine(CanvasGroup canvasGroup)
    {
        while (disappearCurrentTime < fadeDuration)
        {
            disappearCurrentTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1, 0, disappearCurrentTime / fadeDuration);
            canvasGroup.alpha = newAlpha;

            if(canvasGroup.alpha == 0)
            {
                StopCoroutine(DisappearCoroutine(canvasGroup));
                canvasGroup.gameObject.SetActive(false);                
            }
           
            yield return null;
        }
    }
}