using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLoopScript : MonoBehaviour
{
    [Range(-100f, 100f)]
    public float scrollspeed = 1f;
    private float _offset;
    private Material _mat;

    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _offset += (Time.deltaTime * scrollspeed) / 10f;
        _mat.SetTextureOffset("_MainTex", new Vector2(_offset, 0));
    }


}
