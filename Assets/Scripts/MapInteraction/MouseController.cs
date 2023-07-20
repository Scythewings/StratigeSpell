using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MouseController : MonoBehaviour
{
    public float speed;
    public GameObject characterPrefab;

    private CharacterInfo _character;
    private PathFinder _pathFinder;
    private List<OverlayTiles> _path = new List<OverlayTiles>();
    // Start is called before the first frame update
    void Start()
    {
        _pathFinder = new PathFinder();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var focusTilesHits = GetFocusedOnTile();

        if (focusTilesHits.HasValue)
        { 
            OverlayTiles overlayTile = focusTilesHits.Value.collider.GetComponent<OverlayTiles>();
            transform.position = overlayTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
            if (Input.GetButtonDown("Fire1"))
            {
                overlayTile.GetComponent<OverlayTiles>().ShowTiles();
                if (_character == null)
                {
                    _character = Instantiate(characterPrefab).GetComponentInChildren<CharacterInfo>();
                    PositionCharacterOnLine(overlayTile);
                }
                else
                {
                    var _path = _pathFinder.FindPath(_character.activeTile, overlayTile);
                }
            }
        }
        if(_path.Count >0)
        {
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        var step = speed * Time.deltaTime;
        var zIndex = _path[0].transform.position.z;
        _character.transform.position = Vector2.MoveTowards(_character.transform.position, _path[0].transform.position, step);
        _character.transform.position = new Vector3(_character.transform.position.x, _character.transform.position.y, zIndex);
        if (Vector2.Distance(_character.transform.position, _path[0].transform.position) <0.0001f)
        {
            PositionCharacterOnLine(_path[0]);
            _path.RemoveAt(0);
        }
    }

    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);
        if (hits.Length > 0)
        {
            return hits.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        return null;
    }
    private void PositionCharacterOnLine (OverlayTiles tile)
    {
        _character.transform.position = new Vector3(tile.transform.position.y+0.001f, tile.transform.position.z);
        _character.GetComponentInChildren<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        _character.activeTile = tile;
    }
}
