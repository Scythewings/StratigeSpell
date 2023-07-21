using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTiles : MonoBehaviour
{
    public int G;
    public int H;

    public bool isBlocked;

    public OverlayTiles previous;

    public Vector3Int gridLocation;

    public int F
    {
        get
        {
            return G + H;
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            HideTiles();
        }
    }

    /*public void ShowTiles()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
    }
    */

    public void HideTiles() //work
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
}
