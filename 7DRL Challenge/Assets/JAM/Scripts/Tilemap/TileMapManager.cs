using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using JAM.Manager.Pathfinding;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Singleton;
using Random = UnityEngine.Random;

namespace JAM.TileMap
{
    public class TileMapManager : MonoBehaviourSingleton<TileMapManager>
    {
        #region Private Variables
        [Header("Floor")]
        [Tooltip("Minimum tile size for the tileMap.")]
        [SerializeField] private int _minTileSize = 10;
        [Tooltip("Maximum tile size for the tileMap.")]
        [SerializeField] private int _maxTileSize = 15;
        [Tooltip("TileMap to generate random tiles.")]
        [SerializeField] private Tilemap _tileMap;
        [Tooltip("Tile to generate on the tileMap.")]
        [SerializeField] private Tile[] _tiles;
        [Header("Obstacles")]
        [Tooltip("Minimum obstacles to generate.")]
        [SerializeField] private int _minObstacle = 3;
        [Tooltip("Maximum obstacles to generate.")]
        [SerializeField] private int _maxObstacle = 5;
        [Tooltip("Tile to generate as an obstacle.")]
        [SerializeField] private Tile _obstacleTile;

        [Tooltip("Horizontal tile size for the tileMap.")]
        private int _horizontalTileSize;
        [Tooltip("Vertical tile size for the tileMap.")]
        private int _verticalTileSize;
        [Tooltip("Flag to check if the tileMap is generated.")]
        private bool _isTileMapGenerated;
        [Tooltip("List of obstacle positions.")]
        private readonly List<Vector3Int> _obstaclePositions = new();

        bool _isReady;

        public Tilemap Tilemap => _tileMap;

        #endregion

        #region Properties
        public int HorizontalTileSize => _horizontalTileSize;
        public int VerticalTileSize => _verticalTileSize;
        #endregion
        
        #region Events
        public Action OnTilesGenerated;
        #endregion
        
        #region MonoBehaviour Callbacks
        protected override void Awake()
        {
            base.Awake();
            
            GetNewTileSize();
            GenerateRandomTileMap();
        }

        #region TEMPORAL
        private void Update()
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (hit.collider != null)
                {
                    var tilePosition = _tileMap.WorldToCell(hit.point);
                    if (!IsInsideBounds(tilePosition)) { return; }
                    
                    //IsObstacle(tilePosition);
                }
            }
        }
        #endregion
        #endregion
        
        #region Public Methods

        public bool IsDistanceValid(Vector3Int start, Vector3Int end, int distance)
        {
            var path = AStarManager.Instance.CreatePath(start, end);
            return path.Count < distance;  
        }

        /// <summary>
        /// Generate random tileMap with random tiles.
        /// </summary>
        [Button]
        public void GenerateRandomTileMap()
        {
            // Clear previous tileMap
            if (_isTileMapGenerated)
            {
                ClearTileMap();
            }
            
            //Get new tile size
            GetNewTileSize();
            
            // Choose random tile and paint the tileMap
            var tile = _tiles[Random.Range(0, _tiles.Length)];
            for (var x = 0; x < _horizontalTileSize; x++)
            {
                for (var y = 0; y < _verticalTileSize; y++)
                {
                    var tilePosition = new Vector3Int(x, y, 0);
                    _tileMap.SetTile(tilePosition, tile);
                }
            }

            _isTileMapGenerated = true;
            _isReady = true;
            // Generate obstacles
            GenerateObstacles();
            OnTilesGenerated?.Invoke();
        }
        
        /// <summary>
        /// Clear all tiles from the tileMap.
        /// </summary>
        [Button]
        public void ClearTileMap()
        {
            _tileMap.ClearAllTiles();
            _isTileMapGenerated = false;
            _obstaclePositions.Clear();
        }
        
        /// <summary>
        /// Paint a tile on the tileMap.
        /// </summary>
        /// <param name="position"> Position to paint the tile. </param>
        /// <param name="tile"> Tile to paint. </param>
        public void PaintTile(Vector3Int position, Tile tile)
        {
            _tileMap.SetTile(position, tile);
        }

        public void SubscribeToTileMapGenerated(Action tileMapGenerated) 
        {
            if (_isReady) 
            {
                tileMapGenerated?.Invoke();
            }
            else
            {
                OnTilesGenerated -= tileMapGenerated;
                OnTilesGenerated += tileMapGenerated;
            }
        }

        public void UnsubscribeToTileMapGenerated(Action tileMapGenetared) 
        {
            OnTilesGenerated -= tileMapGenetared;
        }
        
        /// <summary>
        /// Check if the position is an obstacle.
        /// </summary>
        /// <param name="position"> Position to check. </param>
        /// <returns></returns>
        public bool IsObstacle(Vector3Int position)
        {
            return _obstaclePositions.Contains(position);
        }

        public bool IsInsideBounds(Vector3Int tilePosition)
        {
            return tilePosition.x >= 0 && 
                   tilePosition.x < _horizontalTileSize && 
                   tilePosition.y >= 0 &&
                   tilePosition.y < _verticalTileSize;
        }
        
        public Vector3Int GetTilePosition(Vector3 worldPosition)
        {
            return _tileMap.WorldToCell(worldPosition);
        }
        
        public Vector3 GetWorldPosition(Vector3Int tilePosition)
        {
            return _tileMap.GetCellCenterWorld(tilePosition);
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Get new random tile size for the tileMap.
        /// </summary>
        private void GetNewTileSize()
        {
            _horizontalTileSize = Random.Range(_minTileSize, _maxTileSize);
            _verticalTileSize = Random.Range(_minTileSize, _maxTileSize);
        }

        /// <summary>
        /// Generate obstacles on the tileMap.
        /// </summary>
        private void GenerateObstacles()
        {
            // Get total obstacles to generate
            var totalObstacles = Random.Range(_minObstacle, _maxObstacle);
            
            for (var i = 0; i < totalObstacles; i++)
            {
                var canPlaceObstacle = false;
                var tilePosition = new Vector3Int();
                
                // Check if the position is near an obstacle
                while (!canPlaceObstacle)
                {
                    // Get random position
                    tilePosition = GetRandomTilePosition();
                    // If the position is near an obstacle, get another position
                    canPlaceObstacle = !GetIfNearObstacle(tilePosition);
                }
                // Add the obstacle position to the list
                _obstaclePositions.Add(tilePosition);
                // Paint the obstacle on the tileMap
                _tileMap.SetTile(tilePosition, _obstacleTile);
            }
        }
        
        /// <summary>
        /// Get random tile position.
        /// </summary>
        /// <returns> Random tile position. </returns>
        public Vector3Int GetRandomTilePosition()
        {
            var x = Random.Range(0, _horizontalTileSize);
            var y = Random.Range(0, _verticalTileSize);
            return new Vector3Int(x, y, 0);
        }
        
        /// <summary>
        /// Check if the position is near an obstacle.
        /// </summary>
        /// <param name="myObstacle"> Position to check. </param>
        /// <returns> True if the position is near an obstacle. </returns>
        private bool GetIfNearObstacle(Vector3Int myObstacle)
        {
            var totalNearObstacles = 0;
            
            // Check immediate neighbors
            for (var xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (var yOffset = -1; yOffset <= 1; yOffset++)
                {
                    var neighborPosition = myObstacle + new Vector3Int(xOffset, yOffset, 0);
                    if (_obstaclePositions.Contains(neighborPosition))
                    {
                        totalNearObstacles++;
                    }
                }
            }
            return totalNearObstacles > 1;
        }
        #endregion


        #region Helpers
        public TileSelectionData Selection(out bool success)
        {
            var data = new TileSelectionData();

            var hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit.collider != null)
            {
                var tilePosition = _tileMap.WorldToCell(hit.point);

                data.TilePosition = tilePosition;
                data.Tile = _tileMap.GetTile<Tile>(tilePosition);
                data.IsObstacle = IsObstacle(tilePosition);

                if (tilePosition.x < 0 || tilePosition.x >= _horizontalTileSize || tilePosition.y < 0 || tilePosition.y >= _verticalTileSize)
                {
                    success = false;
                    return data;
                }

                success = true;
            }
            else
            {
                success = false;
            }

            return data;
        }

        public bool IsInBounds(Vector3Int position)
        {
            return position.x >= 0 && position.x < _horizontalTileSize && position.y >= 0 && position.y < _verticalTileSize;
        }

        public Tile GetTile(Vector3Int position)
        {
            return _tileMap.GetTile<Tile>(position);
        }

        public Vector3 GetCellCenter(Vector3Int position)
        {
            return _tileMap.GetCellCenterWorld(position);
        }
        #endregion


        public struct TileSelectionData
        {
            public Vector3Int TilePosition;
            public Tile Tile;
            public bool IsObstacle;
        }
    }
}
