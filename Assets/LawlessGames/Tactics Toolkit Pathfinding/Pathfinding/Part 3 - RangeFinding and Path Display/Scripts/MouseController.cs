using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static finished3.ArrowTranslator;
using static UnityEditor.Progress;
using UnityEngine.TestTools;

namespace finished3
{
    public class MouseController : MonoBehaviour
    {
        public GameObject cursor;
        [HideInInspector] public float speed = 4;

        //Character Setup
        public GameObject[] characterPrefab;
        private int countCharacter => _activeCharacterList.Count;

        //Character Controller
        [SerializeField] private CharacterDetail _activeCharacter;
        [HideInInspector] private List<CharacterDetail> _activeCharacterList = new List<CharacterDetail>();
        private int _activeCharacterIndex = 0;

        private PathFinder pathFinder;
        private RangeFinder rangeFinder;
        private ArrowTranslator arrowTranslator;
        private List<OverlayTile> path;
        private List<OverlayTile> rangeFinderTiles;

        private void Start()
        {
            pathFinder = new PathFinder();
            rangeFinder = new RangeFinder();
            arrowTranslator = new ArrowTranslator();

            path = new List<OverlayTile>();
            rangeFinderTiles = new List<OverlayTile>();
        }

        void LateUpdate()
        {
            RaycastHit2D? hit = GetFocusedOnTile();          

            if (Input.GetKeyDown("q"))
            {
                SwitchCharacter();               
            }

            if (hit.HasValue)
            {
                OverlayTile tile = hit.Value.collider.gameObject.GetComponent<OverlayTile>();
                cursor.transform.position = tile.transform.position;
                cursor.gameObject.GetComponent<SpriteRenderer>().sortingOrder = tile.transform.GetComponent<SpriteRenderer>().sortingOrder;

                if (rangeFinderTiles.Contains(tile) && !_activeCharacter.isMoving && !_activeCharacter.isFreeze)
                {
                    path = pathFinder.FindPath(_activeCharacter.standingOnTile, tile, rangeFinderTiles);

                    ClearArrowPath();

                    for (int i = 0; i < path.Count; i++)
                    {
                        var previousTile = i > 0 ? path[i - 1] : _activeCharacter.standingOnTile;
                        var futureTile = i < path.Count - 1 ? path[i + 1] : null;

                        var arrow = arrowTranslator.TranslateDirection(previousTile, path[i], futureTile);
                        path[i].SetSprite(arrow);
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    tile.ShowTile();

                    if (characterPrefab.Length != countCharacter)
                    {
                        _activeCharacter = Instantiate(characterPrefab[countCharacter]).GetComponent<CharacterDetail>();
                        _activeCharacterList.Add(_activeCharacter);
                        PositionCharacterOnLine(tile);
                        _activeCharacter.standingOnTile.isBlocked = true;
                        _activeCharacterIndex = countCharacter;                        
                    }
                    else
                    {
                        _activeCharacter.isMoving = true;
                        tile.gameObject.GetComponent<OverlayTile>().HideTile();
                    }
                }
            }            

            if (path.Count > 0 && _activeCharacter.isMoving)
            {
                MoveAlongPath();               
            }            

        }

        private void MoveAlongPath()
        {
            _activeCharacter.standingOnTile.isBlocked = false;
            var step = speed * Time.deltaTime;

            float zIndex = path[0].transform.position.z;
            _activeCharacter.transform.position = Vector2.MoveTowards(_activeCharacter.transform.position, path[0].transform.position, step);
            _activeCharacter.transform.position = new Vector3(_activeCharacter.transform.position.x, _activeCharacter.transform.position.y, zIndex);

            if (Vector2.Distance(_activeCharacter.transform.position, path[0].transform.position) < 0.001f)
            {
                PositionCharacterOnLine(path[0]);
                path.RemoveAt(0);
            }

            if (path.Count == 0)
            {
                ClearArrowPath();
                _activeCharacter.standingOnTile.isBlocked = true;

            }
        }

        private void ClearArrowPath()
        {
            foreach (var item in rangeFinderTiles)
            {
                MapManager.Instance.map[item.grid2DLocation].SetSprite(ArrowDirection.None);                
            }
        }

        private void SwitchCharacter()
        {
            //Reset last character
            ClearArrowPath();
            _activeCharacter.isMoving = false;
            _activeCharacter.isFreeze = false;

            //Swap            
            _activeCharacterIndex = (_activeCharacterIndex + 1) % _activeCharacterList.Count;
            _activeCharacter = _activeCharacterList[_activeCharacterIndex];
            int moverange = _activeCharacter.numberOfMovement;

            //Set up
            if (_activeCharacter.isDead)
            {
                SwitchCharacter();
            }
            GetInRangeTiles(moverange);
        }

        private void PositionCharacterOnLine(OverlayTile tile)
        {
            _activeCharacter.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
            _activeCharacter.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder;
            _activeCharacter.standingOnTile = tile;
        }

        private static RaycastHit2D? GetFocusedOnTile()
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

        private void GetInRangeTiles(int range)
        {
            rangeFinderTiles = rangeFinder.GetTilesInRange(new Vector2Int(_activeCharacter.standingOnTile.gridLocation.x, _activeCharacter.standingOnTile.gridLocation.y), range);
            foreach (var item in rangeFinderTiles)
            {
                item.ShowTile();
            }
        }
    }
}
