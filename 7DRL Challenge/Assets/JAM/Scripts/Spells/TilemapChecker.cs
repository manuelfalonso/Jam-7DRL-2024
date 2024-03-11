using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapChecker
{
    public Tilemap tilemap;
    public Vector3Int centerPosition;



    public bool CheckTilesInCross(Vector3Int center, Vector3Int tile, int distance, Tilemap tilemap)
    {
        for (int i = -distance; i <= distance; i++)
        {
            Vector3Int position = center + new Vector3Int(i, 0, 0);
            if (tilemap.HasTile(position))
            {
                if (position == tile)
                {
                    return true;
                }

            }

            position = center + new Vector3Int(0, i, 0);
            if (tilemap.HasTile(position))
            {
                if (position == tile)
                {
                    return true;
                }

            }
        }
        return false;
    }

    public bool CheckTilesInCrossAndDiagonals(Vector3Int center, Vector3Int tile, int distance, Tilemap tilemap)
    {
        for (int i = -distance; i <= distance; i++)
        {
            for (int j = -distance; j <= distance; j++)
            {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= distance)
                {
                    Vector3Int position = center + new Vector3Int(i, j, 0);
                    if (tilemap.HasTile(position))
                    {
                        if (position == tile)
                        {
                            return true;
                        }

                    }
                }
            }
        }
        return false;
    }

}