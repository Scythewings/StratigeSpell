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
        
    }

    void Update()
    {
        if (redAlive == 0)
        {
            //end blue win
        }
        else if (blueAlive == 0)
        {
            //end red win
        }
    }
}
