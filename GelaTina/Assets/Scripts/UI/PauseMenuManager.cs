using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    private Canvas _pauseCanvas;
    private float _previousTimeScale;
    private bool _isPaused = false;

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
            _isPaused = !_isPaused;

            if(_isPaused == true)
            {
                _pauseCanvas.gameObject.SetActive(true);
                _previousTimeScale = Time.timeScale;
                Time.timeScale = 0;
            }
            else
            {
                _pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = _previousTimeScale;
            }
        }
    }

    public void Save()
    {
        // TODO: implement a save function
        throw new NotImplementedException();
    }

    public void Quit()
    {
        Time.timeScale = _previousTimeScale;

        SceneManager.LoadScene(0);
    }
}
