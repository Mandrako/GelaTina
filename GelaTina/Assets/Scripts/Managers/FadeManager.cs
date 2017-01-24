using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public float Step = 0.1f;

    private static FadeManager _instance;
    private float _transparence;

	void Awake ()
	{
	    if (_instance == null)
	    {
	        _instance = this;
            DontDestroyOnLoad(gameObject);
	    }
	    else
	    {
	        Destroy(gameObject);
	    }
	}

    public static void StartFadeOut()
    {
        _instance.StartCoroutine("FadeOut");
    }

    public static void StartFadeIn()
    {
        _instance.StartCoroutine("FadeIn");
    }

    IEnumerator FadeOut()
    {
        while (_transparence < 1)
        {
            _transparence += Step * Time.deltaTime;
            GetComponent<CanvasGroup>().alpha = _transparence;

            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        while (_transparence > 0)
        {
            _transparence -= Step * Time.deltaTime;
            GetComponent<CanvasGroup>().alpha = _transparence;

            yield return null;
        }
    }
}
