using JAM.Entities.Player;
using JAM.TileMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JAM.Spells
{
    public class SpellsShooter : MonoBehaviour
    {
        public List<Spell> _spells;

        [SerializeField] private Spell _currentSpell;


        void Start()
        {
            if(_spells == null)
            {
                Debug.Log("Upload spells TITAN");
                return;
            }
            
        }
        
        public bool ShootSpell(Vector3Int start, Vector3Int tile) 
        {
            if(TileMapManager.Instance.IsDistanceValid(start, tile, _currentSpell.SpellStats.TilesLength))
            {

                if (_currentSpell.SpellStats.DirectionOfAttack == DirectionOfAttack.CROSS)
                {

                }
                var instaciateSpell = Instantiate(_currentSpell);
                instaciateSpell.Instaciate(tile);
                return true;
            }
            return false;            
        }

        public void SetSpell(string spellName)
        {
            _currentSpell = _spells.Find(spell => spell.name == spellName);

            if(_currentSpell == null) 
            {
                Debug.Log($"No se encontro el hechizo");
                return;
            }

        }

    }
}
