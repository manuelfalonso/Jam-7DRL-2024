using JAM.Entities;
using JAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JAM.TileMap;
using JAM.Manager.Pathfinding;

namespace JAM.Entities.Enemy
{
    public class Enemy : Entity
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Attack _enemyAttack;
        private Vector3Int _newPosition;
        int distanceToPlayer;

        protected override void Awake()
        {
            base.Awake();
            distanceToPlayer = 0;
            _enemyAttack.gameObject.SetActive(false);
        }

        private void Start()
        {
            TurnSystem.Instance.AddEnemyToList(this);
            _newPosition = TileMapManager.Instance.GetTilePosition(transform.position);
            transform.position = _newPosition;
        }

        private void Move() 
        {
            var tilePosition = TileMapManager.Instance.GetTilePosition(_player.transform.position);
            if (!TileMapManager.Instance.IsInsideBounds(tilePosition) ||
                TileMapManager.Instance.IsObstacle(tilePosition)) { return; }
            var path = AStarManager.Instance.CreatePath(_newPosition, tilePosition);
            if (path == null) { return; }
            if (path.Count == 1) 
            {
                distanceToPlayer = 1;
                return; 
            }
            distanceToPlayer = (path.Count - 1) - 3;
            if (distanceToPlayer < 3) 
            {
                distanceToPlayer = 1;
            }
            var pathSelected = path[distanceToPlayer];
            _newPosition = new Vector3Int(pathSelected.X, pathSelected.Y, 0);
            var pos = TileMapManager.Instance.GetWorldPosition(
                new Vector3Int(pathSelected.X, pathSelected.Y, 0));
            transform.position = pos;
        }

        private void Attack() 
        {
            if (distanceToPlayer == 1) 
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
            this.gameObject.SetActive(false);
        }
    }
}
