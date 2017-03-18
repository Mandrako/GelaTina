using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickToContinue : MonoBehaviour
{
    public string scene;

    private bool _loadLock;

    void Update()
    {
        if (Input.anyKeyDown && !_loadLock)
        {
            if (string.IsNullOrEmpty(scene))
            {
                scene = SceneManager.GetActiveScene().name;
            }

            LoadScene();
        }
    }

    void LoadScene()
    {
        _loadLock = true;

        SceneManager.LoadScene(scene);
    }
}
