using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTiles : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            HideTiles();
        }
    }

    public void ShowTiles()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
    }

    public void HideTiles()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
}
