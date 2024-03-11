using System;
using JAM.Pathfinding;
using JAM.TileMap;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils.Singleton;

namespace JAM.Manager.Pathfinding
{
    public class AStarManager : MonoBehaviourSingleton<AStarManager>
    {
        [SerializeField] private Tilemap _tileMap;
        [SerializeField] private Tile _invisibleTile;
        public Action OnPathCalculated;
        private Vector3Int[,] _spots;
        private Astar _aStar;
        private BoundsInt _bounds;

        private void OnEnable()
        {
            TileMapManager.Instance.SubscribeToTileMapGenerated(RecalculateBounds);
        }

        private void OnDisable()
        {
            TileMapManager.Instance.UnsubscribeToTileMapGenerated(RecalculateBounds);
        }
        
        public List<Spot> CreatePath(Vector3Int myPos, Vector3Int finalPos)
        {
            var initialPos = new Vector2Int(myPos.x, myPos.y);
            var endPos = new Vector2Int(finalPos.x, finalPos.y);
            var path = _aStar.CreatePath(_spots, initialPos, endPos, 1000);

            if (path is null || path.Count <= 0) { return null; }

            return path;
        }

        private void CreateGrid()
        {
            _spots = new Vector3Int[_bounds.size.x, _bounds.size.y];
            for (int x = _bounds.xMin, i = 0; i < (_bounds.size.x); x++, i++)
            {
                for (int y = _bounds.yMin, j = 0; j < (_bounds.size.y); y++, j++)
                {
                    if (_tileMap.HasTile(new Vector3Int(x, y, 0)))
                    {
                        _spots[i, j] = new Vector3Int(x, y, 0);
                    }
                    else
                    {
                        _spots[i, j] = new Vector3Int(x, y, 1);
                    }
                }
            }
        }
        
        private void RecalculateBounds()
        {
            GenerateTiles();
            _tileMap.CompressBounds();
            _bounds = _tileMap.cellBounds;

            CreateGrid();
            _aStar = new Astar(_bounds.size.x, _bounds.size.y);
            OnPathCalculated?.Invoke();
        }

        private void GenerateTiles()
        {
            for (var x = 0; x < TileMapManager.Instance.HorizontalTileSize; x++)
            {
                for (var y = 0; y < TileMapManager.Instance.VerticalTileSize; y++)
                {
                    var pos = new Vector3Int(x, y, 0);
                    if (TileMapManager.Instance.IsObstacle(pos)) { continue; }

                    _tileMap.SetTile(pos, _invisibleTile);
                }
            }
        }
        
        #region EXAMPLE

        /*void Update()
        {

            if (Input.GetMouseButton(1))
            {

                var hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                var tilePosition = new Vector3Int();
                if (hit.collider != null)
                {
                    tilePosition = tilemap.WorldToCell(hit.point);
                    if (tilePosition.x < 0 || tilePosition.x >= TileMapManager.Instance.HorizontalTileSize|| tilePosition.y < 0 || tilePosition.y >= TileMapManager.Instance.VerticalTileSize)
                    {
                        return;
                    }

                    //IsObstacle(tilePosition);
                }

                Vector3 world = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tilemap.WorldToCell(world);
                start = new Vector2Int(tilePosition.x, tilePosition.y);

            }
            if (Input.GetMouseButton(2))
            {
                Vector3 world = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tilemap.WorldToCell(world);
                roadMap.SetTile(new Vector3Int(gridPos.x, gridPos.y, 0), null);
            }
            if (Input.GetMouseButton(0))
            {

                var hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                var tilePosition = new Vector3Int();
                if (hit.collider != null)
                {
                    tilePosition = tilemap.WorldToCell(hit.point);
                    if (tilePosition.x < 0 || tilePosition.x >= TileMapManager.Instance.HorizontalTileSize || tilePosition.y < 0 || tilePosition.y >= TileMapManager.Instance.VerticalTileSize)
                    {
                        return;
                    }

                    //IsObstacle(tilePosition);
                }


                CreateGrid();

                Vector3 world = camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int gridPos = tilemap.WorldToCell(world);

                if (roadPath is not null && roadPath.Count > 0)
                    roadPath.Clear();

                roadPath = astar.CreatePath(spots, start, new Vector2Int(tilePosition.x, tilePosition.y), 1000);
                if (roadPath is null)
                    return;

                start = new Vector2Int(tilePosition.x, tilePosition.y);
            }

        }*/

        #endregion
    }
}