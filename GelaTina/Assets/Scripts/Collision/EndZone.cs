using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour {
    public string targetTag = "Player";
    public int nextSceneIndex = -1;

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == targetTag)
        {
            OnEnterZone(target.gameObject);
        }
    }

    protected void OnEnterZone(GameObject target)
    {
        Scene nextScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);

        Debug.Log("nextScene: " + nextScene.name + " - " + nextScene.buildIndex);

        if (nextScene.buildIndex != -1)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("Can't find next scene!");
        }
    }
}
