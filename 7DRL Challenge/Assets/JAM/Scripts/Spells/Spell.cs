using JAM.Entities.Enemy;
using JAM.Entities.Player;
using JAM.Spells;
using JAM.TileMap;
using UnityEngine;

namespace JAM
{
    public class Spell : MonoBehaviour
    {
        [SerializeField] private SpellsStats _spellStats;
        [SerializeField] private bool _forwardSpell;
        [SerializeField] private float _speed;

        private Player player;
        private delegate void _moveSpell();
        private Vector3Int _goTo;

        public SpellsStats SpellStats => _spellStats;
        public void Instaciate(Vector3Int pos)
        {
            _goTo = pos;
        }

        void Update()
        {
            if (_spellStats.AttackSpell is TypeOfSpell.ATTACK)
            {
                AttackTranslateSpell();
            }
            else if (_spellStats.AttackSpell is TypeOfSpell.MOVEMENT)
            {
                MovementSpell();
            }
            else
            {
                Debug.Log("Spell is not a movement or a attackSpell");
            }
        }

        private Vector3 worldHit;
        private void OnEnable()
        {

        }

        private void AttackTranslateSpell()
        {
            if (_spellStats.AOE)
            {
                worldHit = TileMapManager.Instance.Tilemap.CellToWorld(_goTo);
                transform.position = Vector3.MoveTowards(transform.position, worldHit, _speed * Time.deltaTime);
                //Debug.Log($"MyPosIs:{transform.position}, and the pos i have to go : {_goTo}");
                if (transform.position == worldHit)
                {
                    AttackDamageAOESpell();
                }
            }
            if (_spellStats.Pierce)
            {
                var actualPos = TileMapManager.Instance.Tilemap.WorldToCell(transform.position);
                transform.position = Vector3.MoveTowards(transform.position, _goTo, _speed * Time.deltaTime);
                

                if(TileMapManager.Instance.IsObstacle(actualPos))
                {
                    Destroy(gameObject);
                }
                AttackDamagePierceSpell();
            }
            else
            {
                //esto es melee del bicho
                //hacer sonido y vfx de dano
                DamageData data = new DamageData();
                data.Amount = _spellStats.Damage;
                Player.Instance.TryTakeDamage(data);
            }

        }

        private void AttackDamageAOESpell()
        {
            var centerOfDamage = _goTo;

            for (var xOffset = -1; xOffset <= 1; xOffset++)
            {
                for (var yOffset = -1; yOffset <= 1; yOffset++)
                {
                    var neighborPosition = centerOfDamage + new Vector3Int(xOffset, yOffset, 0);
                    foreach (Enemy enemy in TurnSystem.Instance.enemies)
                    {
                        if (enemy.MyPosition == neighborPosition)
                        {
                            DamageData data = new DamageData();
                            data.Amount = _spellStats.Damage;
                            enemy.TryTakeDamage(data);
                        }
                    }
                }
            }
            Destroy(gameObject);
        }

        private void AttackDamagePierceSpell()
        {
            if (_spellStats.EnemyAttack)
            {
                if (Player.Instance.CurrentPositionInTile == _goTo)
                {
                    DamageData data = new DamageData();
                    data.Amount = _spellStats.Damage;
                    Player.Instance.TryTakeDamage(data);
                }
            }
            if (_spellStats.EnemyAttack)
            {
                foreach (Enemy enemy in TurnSystem.Instance.enemies)
                {
                    if (enemy.MyPosition == _goTo)
                    {
                        DamageData data = new DamageData();
                        data.Amount = _spellStats.Damage;
                        enemy.TryTakeDamage(data);
                    }
                }
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
