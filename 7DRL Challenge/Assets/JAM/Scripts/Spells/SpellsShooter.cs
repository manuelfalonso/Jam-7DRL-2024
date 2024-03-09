using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JAM.Spells
{
    public class SpellsShooter : MonoBehaviour
    {
        public List<Spell> _spells;

        private Spell _currentSpell;


        void Start()
        {
            if(_spells == null)
            {
                Debug.Log("Upload spells TITAN");
                return;
            }
        }

        public void ShootSpell() 
        {
            Instantiate(_currentSpell);
            
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
