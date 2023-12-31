using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static finished3.ArrowTranslator;
//using static UnityEditor.Progress;
using UnityEngine.TestTools;
using UnityEngine.TextCore.Text;

namespace finished3
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private CheckTeam checkTeam;

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
        private AnimationController animController;
        private ArrowTranslator arrowTranslator;
        private List<OverlayTile> path;
        private List<OverlayTile> rangeFinderTiles;
        [SerializeField] private float _characterTime = 10f;
        public bool startTimer = false;
        public bool allCharacter;
        public bool pauseGame;

        private void Start()
        {
            pathFinder = new PathFinder();
            rangeFinder = new RangeFinder();
            arrowTranslator = new ArrowTranslator();

            path = new List<OverlayTile>();
            rangeFinderTiles = new List<OverlayTile>();
            animController = new AnimationController(); 
        }

        private void FixedUpdate()
        {
            foreach (CharacterDetail character in _activeCharacterList)
            {
                if (character.isDead)
                {
                    animController.AnimPlay(character.GetComponent<Animator>(), AnimationController.CharacterAnim.Dead);
                    character.standingOnTile.isBlocked = false;

                    if (character.team == "Red")
                    {
                        checkTeam.redAlive--;
                    }
                    else if (character.team == "Blue")
                    {
                        checkTeam.blueAlive--;
                    }
                    _activeCharacterList.Remove(character);
                }
            }
        }

        void LateUpdate()
        {
            RaycastHit2D? hit = GetFocusedOnTile();

            if (!pauseGame)
            {

                if (Input.GetKeyDown("q") || _characterTime <= 0)
                {
                    SwitchCharacter();
                    startTimer = true;
                }

                if (startTimer)
                {
                    _characterTime -= Time.deltaTime;
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

                        if (characterPrefab.Length != countCharacter && !allCharacter)
                        {
                            _activeCharacter = Instantiate(characterPrefab[countCharacter]).GetComponent<CharacterDetail>();
                            _activeCharacter.GetComponentInChildren<HealthBar>();
                            _activeCharacterList.Add(_activeCharacter);
                            PositionCharacterOnLine(tile);
                            _activeCharacter.standingOnTile.isBlocked = true;
                            _activeCharacterIndex = countCharacter;

                            if (countCharacter % 2 == 0)
                            {
                                _activeCharacter.team = "Red";
                                checkTeam.redAlive++;
                            }
                            else
                            {
                                _activeCharacter.team = "Blue";
                                checkTeam.blueAlive++;
                            }
                        }
                        else
                        {
                            checkTeam.justStartGame = true;
                            allCharacter = true;
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
        }

        private void MoveAlongPath()
        {
            _activeCharacter.standingOnTile.isBlocked = false;
            animController.AnimPlay(_activeCharacter.GetComponent<Animator>(), AnimationController.CharacterAnim.Walk);
            var step = speed * Time.deltaTime;

            //animController.AnimPlay(_activeCharacter.GetComponent<Animator>(), AnimationController.CharacterAnim.Walk);
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
            _characterTime = 10f;
            CloseInRangeTiles(_activeCharacter.numberOfMovement);


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
            _activeCharacter.attackMode = false;
            if (_activeCharacter.skillscountDown > 0)
            {
                _activeCharacter.skillscountDown--;
            }

            if (_activeCharacter.isProtected)
            {
                _activeCharacter.protectedTime--;
                if (_activeCharacter.protectedTime == 0)
                {
                    _activeCharacter.protectedTime = 2;
                    _activeCharacter.isProtected = false;
                }
            }
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

        private void CloseInRangeTiles(int range)
        {
            rangeFinderTiles = rangeFinder.GetTilesInRange(new Vector2Int(_activeCharacter.standingOnTile.gridLocation.x, _activeCharacter.standingOnTile.gridLocation.y), range);
            foreach (var item in rangeFinderTiles)
            {
                item.HideTile();
            }
        }

        public void PauseGame()
        {
            pauseGame = !pauseGame;
        }
    }
}
