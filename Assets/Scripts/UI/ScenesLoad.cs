using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoad : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    [SerializeField] int _sceneIndex = 0;
    public int sceneIndex => _sceneIndex;

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int SceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneIndex);
    }
}
