using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoad : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextScene()
    {
        StartCoroutine(LoadingScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadingScene(int SceneIndex) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneIndex);
    }
}
