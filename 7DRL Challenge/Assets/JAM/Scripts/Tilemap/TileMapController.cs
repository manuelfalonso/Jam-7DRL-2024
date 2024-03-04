using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace JAM.TileMap
{
    public class TileMapController : MonoBehaviour
    {
        #region Private Variables
        [Tooltip("Minimum tile size for the tileMap.")]
        [SerializeField] private int _minTileSize = 10;
        [Tooltip("Maximum tile size for the tileMap.")]
        [SerializeField] private int _maxTileSize = 15;
        [Tooltip("TileMap to generate random tiles.")]
        [SerializeField] private Tilemap _tileMap;
        [Tooltip("Tile to generate on the tileMap.")]
        [SerializeField] private Tile _tile;
        [Tooltip("Horizontal tile size for the tileMap.")]
        private int _horizontalTileSize;
        [Tooltip("Vertical tile size for the tileMap.")]
        private int _verticalTileSize;
        [Tooltip("Flag to check if the tileMap is generated.")]
        private bool _isTileMapGenerated;
        #endregion

        #region MonoBehaviour Callbacks
        private void Awake()
        {
            GetNewTileSize();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generate random tileMap with random tiles.
        /// </summary>
        [Button]
        private void GenerateRandomTileMap()
        {
            if (_isTileMapGenerated)
            {
                ClearTileMap();
            }
            
            GetNewTileSize();
            for (var x = 0; x < _horizontalTileSize; x++)
            {
                for (var y = 0; y < _verticalTileSize; y++)
                {
                    var tilePosition = new Vector3Int(x, y, 0);
                    _tileMap.SetTile(tilePosition, _tile);
                }
            }

            _isTileMapGenerated = true;
        }
        
        /// <summary>
        /// Clear all tiles from the tileMap.
        /// </summary>
        [Button]
        private void ClearTileMap()
        {
            _tileMap.ClearAllTiles();
            _isTileMapGenerated = false;
        }
        
        /// <summary>
        /// Get new random tile size for the tileMap.
        /// </summary>
        private void GetNewTileSize()
        {
            _horizontalTileSize = Random.Range(_minTileSize, _maxTileSize);
            _verticalTileSize = Random.Range(_minTileSize, _maxTileSize);
            
        }
        #endregion
    }
}
