using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    private Canvas _pauseCanvas;

	void Start ()
	{
	    _pauseCanvas = GetComponentInChildren<Canvas>();
	    if (_pauseCanvas != null)
	    {
	        _pauseCanvas.gameObject.SetActive(false);
	    }
	    else
	    {
	        Debug.LogError("Couldn't find a canvas for the pause menu in children!");
	    }
	}

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _pauseCanvas.gameObject.SetActive(!_pauseCanvas.gameObject.activeSelf);
        }
    }

    public void Save()
    {
        // TODO: implement a save function
        throw new NotImplementedException();
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
