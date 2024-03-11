using System.Collections;
using JAM.Spells;
using JAM.Stats;
using JAM.TileMap;
using UnityEngine;
using Utils.Singleton;

namespace JAM.Entities._Player
{
    public class Player : Entity
    {
        [SerializeField] private Vector3Int _currentPosition;
        [SerializeField] private SpellsShooter _spellShooter;
        
        private static Player _instance;
        private PlayerInputs _inputActions;
        private int _leftMoves;
        
        public Vector3Int CurrentPositionInTile => _currentPosition;
        public static Player Instance => _instance;

        protected override void Awake()
        {
            base.Awake();
            
            _instance = this;
            
            _inputActions = new PlayerInputs();
            _inputActions.Enable();
            _inputActions.Player.Move.started += ctx => CheckIfCanMove(ctx.ReadValue<Vector2>());
            
            TurnSystem.Instance.onTurnStart += GetMovementValues;
            
            GetMovementValues();
        }
        
        private void Start()
        {
            SetStartPosition();
        }
        
        private void Update()
        {
            if (TurnSystem.Instance.IsPlayerTurn() && !TurnSystem.Instance.HasPlayerAttacked())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                    if (hit.collider != null)
                    {
                        var tile = TileMapManager.Instance.Tilemap.WorldToCell(hit.point);
                        if (!TileMapManager.Instance.IsInsideBounds(tile)) { return; }

                        //IsObstacle(tilePosition);
                        if(_spellShooter.ShootSpell(_currentPosition, tile))
                        {
                            TurnSystem.Instance.onAttack?.Invoke();
                        }
                    }
                }
            }

        }

        private void SetStartPosition()
        {
            while (TileMapManager.Instance.IsObstacle(_currentPosition)
                && TileMapManager.Instance.IsInBounds(_currentPosition))
            {
                _currentPosition.x++;
            }

            transform.position = TileMapManager.Instance.GetCellCenter(_currentPosition);
        }

        private void GetMovementValues()
        {
            StatContainer.TryGetValue("TotalMovementTiles", out var moves);
            if (moves is not IntStat intStat)
            {
                Debug.LogError("Couldn't find TotalMovementTiles stat");
                return;
            }
            _leftMoves = intStat.GetValue();
        }
        
        private void CheckIfCanMove(Vector2 pos)
        {
            if (TurnSystem.Instance.IsPlayerTurn() && 
                !TurnSystem.Instance.HasPlayerMoved() &&
                _leftMoves > 0)
            {
                Move(pos);
            }
        }
        
        private void Move(Vector2 pos)
        {
            var myPosInGrid = TileMapManager.Instance.GetTilePosition(transform.position);
            var newPos = myPosInGrid + new Vector3Int((int)pos.x, (int)pos.y, 0);
            
            if(!TileMapManager.Instance.IsInBounds(newPos) ||
               TileMapManager.Instance.IsObstacle(newPos)) { return; }
            
            transform.position = TileMapManager.Instance.GetCellCenter(newPos);
            _currentPosition = newPos;
            _leftMoves--;
        }

        protected override void DeathOfEntity(float toCall)
        {
            base.DeathOfEntity(toCall);
            WinLoseCondition.Instance.onLose?.Invoke();
        }
    }
}
