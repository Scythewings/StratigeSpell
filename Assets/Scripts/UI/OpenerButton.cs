using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenerButton : MonoBehaviour
{
    public GameObject Image;

    public void OpenPanel()
    {
        if (Image != null)
        {
            bool isActive = Image.activeSelf;
            Image.SetActive(!isActive);
        }
    }
}
