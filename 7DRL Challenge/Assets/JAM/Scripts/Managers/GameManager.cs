using System.Collections.Generic;
using JAM.Entities.Enemy;
using JAM.Entities._Player;
using JAM.TileMap;
using UnityEngine;
using Utils.Singleton;
using Random = UnityEngine.Random;

namespace JAM.Manager.Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [SerializeField] private int _initialDistanceToPlayer = 4;
        [SerializeField] private int _minEnemies = 1;
        [SerializeField] private int _maxEnemies = 5;
        [SerializeField] private List<Enemy> _enemies = new();
        private int _totalEnemies;

        private void OnEnable()
        {
            TileMapManager.Instance.SubscribeToTileMapGenerated(MapRecalculated);
        }

        private void OnDisable()
        {
            TileMapManager.Instance.UnsubscribeToTileMapGenerated(MapRecalculated);
        }

        private void MapRecalculated()
        {
            _totalEnemies = Random.Range(_minEnemies, _maxEnemies - 1);
            var playerPos = Player.Instance.CurrentPositionInTile;
            for (var i = 0; i < _totalEnemies; i++)
            {
                var enemy = _enemies[Random.Range(0, _enemies.Count)];
                var validPlace = false;
                while (!validPlace)
                {
                    var pos = TileMapManager.Instance.GetRandomTilePosition();
                    if (TileMapManager.Instance.IsObstacle(pos) ||
                        TileMapManager.Instance.IsDistanceValid(pos, playerPos, _initialDistanceToPlayer))
                    {
                        continue;
                    }
                    
                    var worldPos = TileMapManager.Instance.GetCellCenter(pos);
                    Instantiate(enemy, worldPos, Quaternion.identity);
                    TurnSystem.Instance.AddEnemy(enemy);
                    validPlace = true;
                }
            }
        }
    }
}
