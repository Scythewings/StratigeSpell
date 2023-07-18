using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _DiffColor;
    [SerializeField] private SpriteRenderer _Renderer;
    [SerializeField] private GameObject _highlight;
    public void Init(bool isoffset)
    {
        _Renderer.color = isoffset ? _baseColor : _DiffColor;
    }
    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
    private void OnMouseExit()
    {
        _highlight.SetActive(false); 
    }

}
