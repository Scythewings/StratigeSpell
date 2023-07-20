using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class BulletTrail : MonoBehaviour
{
    private Vector3 _StartPos;
    private Vector3 _TargetPos;
    private float _progress;
    [SerializeField] private float _Speed = 40f;
    void Start()
    {
        _StartPos = transform.position.WithAxis(VectorExtension.Axis.Z, -1);
    }

    
    void Update()
    {
        _progress += Time.deltaTime * _Speed;
        transform.position = Vector3.Lerp(_StartPos, _TargetPos, _progress);
    }
    public void SetTargetPos(Vector3 targetPos)
    {
        _TargetPos = targetPos.WithAxis(VectorExtension.Axis.Z, -1);
    }
}
