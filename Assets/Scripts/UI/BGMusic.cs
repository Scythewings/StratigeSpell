using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    private static BGMusic _backgroundMusic;
    void Awake()
    {
        if (_backgroundMusic == null)
        {
            _backgroundMusic = this;
            DontDestroyOnLoad(_backgroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
