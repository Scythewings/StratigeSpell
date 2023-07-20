using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool moveUp;
    public bool moveDown;
    public bool moveLeft;
    public bool moveRight;
    public bool openMap;
    public bool attack;
    public bool skills;


    public void Update()
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
