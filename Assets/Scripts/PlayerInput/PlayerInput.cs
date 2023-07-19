using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool moveUp;
    private bool moveDown;
    private bool moveLeft;
    private bool moveRight;
    private bool openMap;
    private bool attack;
    private bool skills;


    private void Update()
    {
        moveUp = Input.GetKeyDown("w");
        moveDown = Input.GetKeyDown("s");
        moveLeft = Input.GetKeyDown("a");
        moveRight = Input.GetKeyDown("d");
        openMap = Input.GetKeyDown("m");
        attack = Input.GetKeyDown("space");
        skills = Input.GetKeyDown("e");
    }



}
