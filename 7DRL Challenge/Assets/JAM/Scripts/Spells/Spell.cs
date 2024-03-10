using JAM.Entities.Player;
using JAM.Spells;
using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace JAM
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] private SpellsStats _spellStats;
        [SerializeField] private bool _forwardSpell;

        private Player player;
        private delegate void _moveSpell();
        private _moveSpell actualSpell;
        private Vector3Int _goTo;
        public void Instaciate(Vector3Int pos)
        {
            _goTo=pos;
        }

        void Update()
        {
            actualSpell();
        }

        private void OnEnable()
        {
            if (_spellStats.AttackSpell is TypeOfSpell.ATTACK)
            {
                actualSpell = AttackTranslateSpell;
            }
            else if (_spellStats.AttackSpell is TypeOfSpell.MOVEMENT)
            {
                actualSpell = MovementSpell;
            }
            else
            {
                Debug.Log("Spell is not a movement or a attackSpell");
            }
        }

        private void AttackTranslateSpell()
        {
            transform.position = Vector3.MoveTowards(transform.position, _goTo, 5f);
            if (transform.position == _goTo)
            {
                actualSpell = null;

            }
        }

        private void MovementSpell()
        {
            //hacer un puff o algo lindo para el spell
            player.gameObject.transform.position = _goTo;
        }

        public string GetName()
        {
            return _spellStats.name;
        }
    }
}
