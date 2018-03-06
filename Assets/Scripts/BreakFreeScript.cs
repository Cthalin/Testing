using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFreeScript : MonoBehaviour {

    public void FadeIn()
    {
        StartCoroutine(DoFadeInPartial());
    }

    IEnumerator DoFadeInPartial()
    {
        float timer = Time.time + 0.3f;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > 0 && Time.time < timer)
        {
            canvasGroup.alpha += Time.deltaTime / 2;

            yield return null;
        }

        //canvasGroup.interactable = false;
        yield return null;
    }
}
