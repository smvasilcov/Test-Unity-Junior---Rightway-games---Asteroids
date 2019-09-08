using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/MainMenu");
        GetComponent<AudioSource>().Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
