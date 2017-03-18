using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [Range(0.1f, 1)]
    public float Step = 0.1f;
    public static bool IsFading = false;

    private float _transparence;
    private CanvasGroup _canvasGroup;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 1;
        _transparence = _canvasGroup.alpha;

        StartFadeIn();
    }

    public void StartFadeOut()
    {
        if (IsFading)
        {
            StopAllCoroutines();
        }

        IsFading = true;
        StartCoroutine("FadeOut");
    }

    public void StartFadeIn()
    {
        if (IsFading)
        {
            StopAllCoroutines();
        }

        IsFading = true;
        StartCoroutine("FadeIn");
    }

    public void Reset()
    {
        ResetFade();
    }

    void ResetFade()
    {
        StopAllCoroutines();
        _canvasGroup.alpha = 0;
        IsFading = false;
    }

    IEnumerator FadeOut()
    {
        while (_transparence < 1)
        {
            _transparence += Step * Time.deltaTime;
            _canvasGroup.alpha = _transparence;

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
            _canvasGroup.alpha = _transparence;

            yield return null;
        }

        IsFading = false;

        yield return null;
    }
}
