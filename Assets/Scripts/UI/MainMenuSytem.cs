using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSytem : MonoBehaviour
{
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GamePlay()
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }
}
