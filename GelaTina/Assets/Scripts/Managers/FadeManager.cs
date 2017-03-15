﻿using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [Range(0.1f, 1)]
    public float Step = 0.1f;
    public static bool IsFading = false;

    private static FadeManager _instance;
    private float _transparence;

	void Awake ()
	{
	    if (_instance == null)
	    {
	        _instance = this;
            DontDestroyOnLoad(transform.parent);
	    }
	    else
	    {
	        Destroy(gameObject);
	    }
	}

    public static void StartFadeOut()
    {
        if (IsFading)
        {
            _instance.StopCoroutine(_instance.FadeOut());
        }

        IsFading = true;
        _instance.StartCoroutine("FadeOut");
    }

    public static void StartFadeIn()
    {
        if (IsFading)
        {
            _instance.StopCoroutine(_instance.FadeIn());
        }

        IsFading = true;
        _instance.StartCoroutine("FadeIn");
    }

    public static void Reset()
    {
        _instance.ResetFade();
    }

    void ResetFade()
    {
        StopAllCoroutines();
        GetComponent<CanvasGroup>().alpha = 0;
        IsFading = false;
    }

    IEnumerator FadeOut()
    {
        while (_transparence < 1)
        {
            _transparence += Step * Time.deltaTime;
            GetComponent<CanvasGroup>().alpha = _transparence;

            yield return null;
        }

        IsFading = false;

        yield return null;
    }

    IEnumerator FadeIn()
    {
        while (_transparence > 0)
        {
            _transparence -= Step * Time.deltaTime;
            GetComponent<CanvasGroup>().alpha = _transparence;

            yield return null;
        }

        IsFading = false;

        yield return null;
    }
}
