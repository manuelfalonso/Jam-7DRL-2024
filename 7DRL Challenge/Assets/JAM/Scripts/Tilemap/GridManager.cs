using JAM.TileMap;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap roadMap;
    public TileBase roadTile;
    public Vector3Int[,] spots;
    // aca sera el enemigo
    public Vector2Int start;
    Astar astar;
    List<Spot> roadPath = new List<Spot>();
    new Camera camera;
    BoundsInt bounds;
    // Start is called before the first frame update
    void Start()
    {
        TileMapManager.Instance.OnTilesGenerated -= RecalculateBoundries;
        TileMapManager.Instance.OnTilesGenerated += RecalculateBoundries;

        tilemap.CompressBounds();
        roadMap.CompressBounds();
        bounds = tilemap.cellBounds;
        camera = Camera.main;

        CreateGrid();
        astar = new Astar(spots, bounds.size.x, bounds.size.y);
    }
    private void OnDisable()
    {
        TileMapManager.Instance.OnTilesGenerated -= RecalculateBoundries;
    }
    public void CreateGrid()
    {
        spots = new Vector3Int[bounds.size.x, bounds.size.y];
        for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
        {
            for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            {
                if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                {
                    spots[i, j] = new Vector3Int(x, y, 0);
                }
                else
                {
                    spots[i, j] = new Vector3Int(x, y, 1);
                }
            }
        }
    }

    private void RecalculateBoundries()
    {
        CleanTilemap(roadMap);

        tilemap.CompressBounds();
        roadMap.CompressBounds();
        bounds = tilemap.cellBounds;
        camera = Camera.main;

        CreateGrid();
        astar = new Astar(spots, bounds.size.x, bounds.size.y);
    }

    private void CleanTilemap(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;
        foreach (var position in bounds.allPositionsWithin)
        {
            // Remove the tile at the current position
            tilemap.SetTile(position, null);
        }
    }

    private void DrawRoad()
    {
        for (int i = 0; i < roadPath.Count; i++)
        {
            roadMap.SetTile(new Vector3Int(roadPath[i].X, roadPath[i].Y, 0), roadTile);
        }
    }

    
    void Update()
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

            if (roadPath != null && roadPath.Count > 0)
                roadPath.Clear();

            roadPath = astar.CreatePath(spots, start, new Vector2Int(tilePosition.x, tilePosition.y), 1000);
            if (roadPath == null)
                return;

            DrawRoad();
            start = new Vector2Int(tilePosition.x, tilePosition.y);
        }
    }
}
