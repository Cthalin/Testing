﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour {

    private bool _haltIn = false;
    private bool _haltOut = false;

	public void FadeOut()
    {
        _haltIn = true;
        _haltOut = false;
        StartCoroutine(DoFadeOut());
    }

    IEnumerator DoFadeOut()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (_haltOut)
        {
            yield break;
        }

        while (canvasGroup.alpha > 0)
        {
            if (_haltOut)
            {
                yield break;
            }
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }

        canvasGroup.interactable = false;
        yield return null;
    }

    public void FadeIn()
    {
        _haltIn = false;
        _haltOut = true;
        StartCoroutine(DoFadeIn());
    }

    IEnumerator DoFadeIn()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (_haltIn)
        {
            yield break;
        }

        while (canvasGroup.alpha < 1)
        {
            if (_haltIn)
            {
                yield break;
            }
            canvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
        }

        canvasGroup.interactable = true;
        yield return null;
    }
}
