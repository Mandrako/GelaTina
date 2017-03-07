using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string NewGameScene;

    public void StartNewGame()
    {
        SceneManager.LoadScene(NewGameScene);
    }

    public void ToggleLoad()
    {
        // TODO: implement a load screen that toggles using this function
        throw new NotImplementedException();
    }

    public void ToggleOptions()
    {
        // TODO: implement an options screen that toggles using this function
        throw new NotImplementedException();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
