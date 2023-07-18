using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private bool _movement;
    private Vector3 _OriginPos, _TargetPos;
    private float _Move = 0.2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && !_movement)
            StartCoroutine(MovePlayer(Vector3.up)); 
        if (Input.GetKey(KeyCode.A) && !_movement)
            StartCoroutine(MovePlayer(Vector3.left));
        if (Input.GetKey(KeyCode.D) && !_movement)
            StartCoroutine(MovePlayer(Vector3.right));
        if (Input.GetKey(KeyCode.S) && !_movement)
            StartCoroutine(MovePlayer(Vector3.down));
            
    }
    private IEnumerator MovePlayer(Vector3 direction)
    {
        _movement = true;
        float elapsedtime = 0;
        _OriginPos = transform.position;
        _TargetPos = _OriginPos + direction;
        while (elapsedtime < _Move)
        {
            transform.position = Vector3.Lerp(_OriginPos, _TargetPos, (elapsedtime/_Move));
            elapsedtime += Time.deltaTime;
            yield return null;
        }
        transform.position = _TargetPos;
        _movement = false;
    }
}
