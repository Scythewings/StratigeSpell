using finished3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckTeam : MonoBehaviour
{
    public int redAlive;
    public int blueAlive;

    void Start()
    {
        
    }

    void Update()
    {
        if (redAlive == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (blueAlive == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
