using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleOptions()
    {
        throw new NotImplementedException();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
