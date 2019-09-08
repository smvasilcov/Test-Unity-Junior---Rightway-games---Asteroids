using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject Canvas;

    public void MainMenu()
    {
        Score.NullScore();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Restart()
    {
        Score.NullScore();
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Resume()
    {
        Pause.ChangePauseState();
    }

    private void Update()       // NEED TO CHANGE ON LISTENERS
    {
        if (Pause.OnPause)
            Canvas.SetActive(true);
        else
            Canvas.SetActive(false);
    }
}
