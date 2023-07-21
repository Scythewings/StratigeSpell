using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float _rotZ;
    public float RotationSpeed;
    public bool ClockRotation;
    void Update()
    {
        if(ClockRotation == false)
        {
            _rotZ += Time.deltaTime * RotationSpeed;

        }
        else
        {
            _rotZ -= Time.deltaTime * RotationSpeed;

        }
        transform.rotation = Quaternion.Euler(0, 0, _rotZ);
    }
}
