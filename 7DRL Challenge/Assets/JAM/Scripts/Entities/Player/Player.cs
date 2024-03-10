using JAM.Stats;
using JAM.TileMap;
using UnityEngine;

namespace JAM.Entities._Player
{
    public class Player : Entity
    {
        [SerializeField] private Vector3Int _initialPosition;
        
        private static Player _instance;
        private PlayerInputs _inputActions;
        private int _leftMoves;
        
        public Vector3Int CurrentPositionInTile => _initialPosition;
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

        private void SetStartPosition()
        {
            while (TileMapManager.Instance.IsObstacle(_initialPosition)
                && TileMapManager.Instance.IsInBounds(_initialPosition))
            {
                _initialPosition.x++;
            }

            transform.position = TileMapManager.Instance.GetCellCenter(_initialPosition);
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
            _initialPosition = newPos;
            _leftMoves--;
        }

        protected override void DeathOfEntity(float toCall)
        {
            base.DeathOfEntity(toCall);
            WinLoseCondition.Instance.onLose?.Invoke();
        }
    }
}
