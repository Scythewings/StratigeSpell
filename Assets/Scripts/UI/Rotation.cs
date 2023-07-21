using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float _rotZ;
    public float rotationSpeed;
    public bool clockRotation;
    void Update()
    {
        if(clockRotation == false)
        {
            _rotZ += Time.deltaTime * rotationSpeed;

        }
        else
        {
            _rotZ -= Time.deltaTime * rotationSpeed;

        }
        transform.rotation = Quaternion.Euler(0, 0, _rotZ);
    }
}
