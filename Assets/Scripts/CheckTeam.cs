using finished3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTeam : MonoBehaviour
{
    public int redAlive;
    public int blueAlive;

    void Start()
    {
        foreach (GameObject v in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (v.TryGetComponent<CharacterDetail>(out CharacterDetail))
            {

            }
        }
    }

    void Update()
    {
        
    }
}
