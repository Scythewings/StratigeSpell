using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MouseController : MonoBehaviour //work
{
    public float speed;
    public GameObject characterPrefab;
    public GameObject cursor;

    private CharacterInfo _character;
    private PathFinder _pathFinder;
    private List<OverlayTiles> _path;
    // Start is called before the first frame update
    void Start()
    {
        _pathFinder = new PathFinder();
        _path = new List<OverlayTiles>();
    }

    // Update is called once per frame
    void LateUpdate() //work
    {
        RaycastHit2D? focusTilesHits = GetFocusedOnTile();

        if (focusTilesHits.HasValue)
        { 
            OverlayTiles overlayTile = focusTilesHits.Value.collider.gameObject.GetComponent<OverlayTiles>();
            cursor.transform.position = overlayTile.transform.position;
            cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder;
            if (Input.GetButtonDown("Fire1"))
            {
                overlayTile.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);

                if (_character == null)
                {
                    _character = Instantiate(characterPrefab).GetComponent<CharacterInfo>();
                    PositionCharacterOnLine(overlayTile);
                    _character.activeTile = overlayTile;
                } 
                else
                {
                 _path = _pathFinder.FindPath(_character.activeTile, overlayTile);
                    overlayTile.gameObject.GetComponent<OverlayTiles>().HideTiles();
                }
            }
        }
        if(_path.Count > 0)
        {
            
            MoveAlongPath();
        }
    }

    private void MoveAlongPath() //work
    {
        var step = speed * Time.deltaTime;
        var zIndex = _path[0].transform.position.z;
        _character.transform.position = Vector2.MoveTowards(_character.transform.position, _path[0].transform.position, step);
        _character.transform.position = new Vector3(_character.transform.position.x, _character.transform.position.y, zIndex);
        if (Vector2.Distance(_character.transform.position, _path[0].transform.position) <0.00001f)
        {
            PositionCharacterOnLine(_path[0]);
            _path.RemoveAt(0);
        }
    }

    public RaycastHit2D? GetFocusedOnTile() //work
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

    private void PositionCharacterOnLine (OverlayTiles tile) //work
    {
        _character.transform.position = new Vector3(tile.transform.position.x , tile.transform.position.y+0.00001f, tile.transform.position.z);
        _character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
        _character.activeTile = tile;
    }
}
