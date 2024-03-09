using JAM.Spells;
using UnityEngine;

namespace JAM
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] private SpellsStats _stats;
        [SerializeField] private bool _forwardSpell;



        public void Instaciate(Direction direc)
        {
            switch (direc)
            {
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                default:
                    break;
            }

            
        }

        void Update()
        {
            //move to direction when we have;






            //Attack from player finished;
        }

        public string GetName()
        {
            return _stats.name;
        }
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
