using JAM.Entities._Player;
using UnityEngine;
using JAM.TileMap;
using JAM.Manager.Pathfinding;
using JAM.Stats;
using System.Collections;
using System.Collections.Generic;

namespace JAM.Entities.Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] private Attack _enemyAttack;
        private Vector3Int _initialPos;
        private int _distanceToPlayer;
        private int _movesLeft;
        private float _timeToMove = 0.5f;

        public Vector3Int MyPosition => _initialPos;
        protected override void Awake()
        {
            base.Awake();
            _distanceToPlayer = 0;
            _enemyAttack.gameObject.SetActive(false);
            
            GetMovementValues();
            GetTimeToMove();
            TurnSystem.Instance.onTurnStart += GetMovementValues;
        }

        private void Start()
        {
            TurnSystem.Instance.AddEnemyToList(this);
            _initialPos = TileMapManager.Instance.GetTilePosition(transform.position);
        }

        private void Move() 
        {
            var tilePosition = TileMapManager.Instance.GetTilePosition(Player.Instance.transform.position);
            if (!TileMapManager.Instance.IsInsideBounds(tilePosition) ||
                TileMapManager.Instance.IsObstacle(tilePosition)) { return; }
            
            var path = AStarManager.Instance.CreatePath(_initialPos, tilePosition);
            
            if (path == null) { return; }
            
            if (path.Count == 1) 
            {
                _distanceToPlayer = 1;
                return; 
            }
            
            _distanceToPlayer = (path.Count - 1) - _movesLeft;
            
            if (_distanceToPlayer < _movesLeft) 
            {
                _distanceToPlayer = 1;
            }
            
            Debug.Log(gameObject.activeInHierarchy);
            StartCoroutine(MovingTowardsPosition(path));
        }

        private IEnumerator MovingTowardsPosition(List<Pathfinding.Spot> path)
        {
            var iterations = 0;
            while (_movesLeft > 0) 
            {
                iterations++;
                var pathSelected = path[^iterations];
                var newPosition = new Vector3Int(pathSelected.X, pathSelected.Y, 0);
                var pos = TileMapManager.Instance.GetWorldPosition(newPosition);
                
                transform.position = pos;
                _movesLeft--;
                yield return new WaitForSeconds(0.3f);
            }
            _initialPos = TileMapManager.Instance.GetTilePosition(transform.position);
        }

        private void Attack() 
        {
            if (_distanceToPlayer == 1) 
            {
                _enemyAttack.gameObject.SetActive(true);
            }
        }

        public void Execute() 
        {
            Move();
            Attack();
        }

        protected override void DeathOfEntity(float toCall)
        {
            base.DeathOfEntity(toCall);
            TurnSystem.Instance.RemoveEnemyFromList(this);
            WinLoseCondition.Instance.onWin?.Invoke();
            gameObject.SetActive(false);
        }
        
        private void GetMovementValues()
        {
            StatContainer.TryGetValue("TotalMovementTiles", out var moves);
            if (moves is not IntStat intStat)
            {
                Debug.LogError("Couldn't find TotalMovementTiles stat");
                return;
            }
            _movesLeft = intStat.GetValue();
        }

        private void GetTimeToMove()
        {
            StatContainer.TryGetValue("TimeToMove", out var time);
            if (time is not FloatStat floatStat)
            {
                Debug.LogError("Couldn't find TimeToMove stat");
                return;
            }
            _timeToMove = floatStat.GetValue();
        } 
    }
}
