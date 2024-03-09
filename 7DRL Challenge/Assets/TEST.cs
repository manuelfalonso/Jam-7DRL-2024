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
                    
                    if (!TileMapManager.Instance.IsInsideBounds(tilePosition) ||
                        TileMapManager.Instance.IsObstacle(tilePosition)) { return; }

                    var path = AStarManager.Instance.CreatePath(_myPosition, tilePosition);
                    
                    if(path == null) { return; }
                    
                    var pathSelected = path[0];
                    _myPosition = new Vector3Int(pathSelected.X, pathSelected.Y, 0);
                    var pos = TileMapManager.Instance.GetWorldPosition(
                        new Vector3Int(pathSelected.X, pathSelected.Y, 0));
                    transform.position = pos;
                }
            }
        }
        
        private Vector3Int TransformToTilePosition()
        {
            return TileMapManager.Instance.GetTilePosition(transform.position);
        }
    }
}
