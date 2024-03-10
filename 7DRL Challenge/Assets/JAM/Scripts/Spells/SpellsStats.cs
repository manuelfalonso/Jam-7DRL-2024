using JAM.Entities.Enemy;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UIElements;

namespace JAM.Spells
{
    [CreateAssetMenu(fileName = "SpellStats", menuName = "AssetJam/SpellStats", order = 1)]
    public class SpellsStats : ScriptableObject
    {
        [SerializeField] private string _spellName;
        [SerializeField] private TypeOfSpell _attackSpell;
        [SerializeField] private Image _image;
        

        [SerializeField] private bool _enemyAttack = false;

        [SerializeField] private int _tilesLength;
        [ShowIf("_attackSpell", TypeOfSpell.ATTACK)]
        [SerializeField] private int _damage;
        [ShowIf("_attackSpell", TypeOfSpell.ATTACK)]
        [SerializeField] private bool _pierce;
        [ShowIf("_attackSpell", TypeOfSpell.ATTACK)]
        [HideIf("_cross")]
        [SerializeField] private bool _AOE;
        [ShowIf("_attackSpell", TypeOfSpell.ATTACK)]
        [HideIf("_AOE")]
        [SerializeField] private bool _cross;
        [ShowIf("_attackSpell", TypeOfSpell.ATTACK)]
        [SerializeField] private DirectionOfAttack _directionOfAttack;

        public TypeOfSpell AttackSpell => _attackSpell;
        public string SpellName => _spellName;
        public int Damage => _damage;
        public int TilesLength => _tilesLength;
        public bool Pierce => _pierce;
        public bool AOE => _AOE;
        public bool Cross => _cross;
        public Image Image => _image;
        public DirectionOfAttack DirectionOfAttack => _directionOfAttack;
        public bool EnemyAttack => _enemyAttack;


    }

    public enum TypeOfSpell
    {
        ATTACK,
        MOVEMENT,
        NONE
    }
    public enum DirectionOfAttack
    {
        CROSS,
        STAR
    }
}
