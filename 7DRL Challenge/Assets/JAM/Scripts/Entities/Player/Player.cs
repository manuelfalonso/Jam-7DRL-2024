using JAM.TileMap;
using UnityEngine;

namespace JAM.Entities.Player
{
    public class Player : Entity
    {
        [SerializeField] private Vector3Int _initialPosition;

        private PlayerInputs _inputActions;


        override protected void Awake()
        {
            base.Awake();

            _inputActions = new PlayerInputs();
        }

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
