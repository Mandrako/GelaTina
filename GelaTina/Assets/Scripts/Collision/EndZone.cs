using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Collision/EndZone")]
public class EndZone : MonoBehaviour {
    public string TargetTag = "Player";
    public string NextSceneName;
    public delegate void OnLoadBegin();
    public event OnLoadBegin OnLoadBeginCallback;

    private bool _isLoading = false;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == TargetTag)
        {
            OnEnterZone(target.gameObject);
        }
    }

    protected void OnEnterZone(GameObject player)
    {
        if (_isLoading == false)
        {
            if(OnLoadBeginCallback != null)
            {
                OnLoadBeginCallback();
            }

            if (string.IsNullOrEmpty(NextSceneName))
            {
                NextSceneName = SceneManager.GetActiveScene().name;
            }

            StartCoroutine(AsyncLoad(NextSceneName));
        }
        
        _isLoading = true;
    }

    IEnumerator AsyncLoad(string sceneName)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.isDone == false)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            if (asyncOperation.progress >= 0.9f && FadeManager.IsFading == false)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
