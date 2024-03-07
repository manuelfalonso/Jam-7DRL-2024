using JAM.TileMap;
using UnityEngine;

namespace JAM.Entities.Player
{
    public class Player : Entity
    {
        [SerializeField] private Vector3Int _initialPosition;


        private void Start()
        {
            SetStartPosition();
        }


        private void SetStartPosition()
        {
            while (TileMapManager.Instance.IsObstacle(_initialPosition)
                && TileMapManager.Instance.IsInBounds(_initialPosition))
            {
                _initialPosition.x++;
            }

            transform.position = TileMapManager.Instance.GetCellCenter(_initialPosition);
        }
    }
}
