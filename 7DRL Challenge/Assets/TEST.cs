using System;
using JAM.TileMap;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JAM
{
    public class TEST : MonoBehaviour
    {
        private Vector3Int _myPosition;
        
        private void Awake()
        {
            _myPosition = TransformToTilePosition();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (hit.collider != null)
                {
                    var tilePosition = TileMapManager.Instance.GetTilePosition(hit.point);
                    if (!TileMapManager.Instance.IsInsideBounds(tilePosition))
                    {
                        return;
                    }

                    var path = AStarManager.Instance.CreatePath(_myPosition, tilePosition);

                    var pathSelected = path[path.Count - 1];
                    transform.position = new Vector3(pathSelected.X, pathSelected.Y, 0);
                }
            }
        }
        
        private Vector3Int TransformToTilePosition()
        {
            return TileMapManager.Instance.GetTilePosition(transform.position);
        }
    }
}
