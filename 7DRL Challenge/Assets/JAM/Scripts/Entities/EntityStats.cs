using Pampero.Stats;
using UnityEngine;

namespace BossRushJam2024.Entities
{
    [CreateAssetMenu(fileName = "New Entity Stats", menuName = "BossRushJam2024/Stats/Entity Stats")]
    public class EntityStats : ScriptableObject
    {
        public IntStat Health;
        public IntStat Damage;
        public IntStat AttackSpeed;
        public IntStat MovementVelocity;
        public IntStat DodgeInvulnerabilityTime;
        public IntStat ParryFrames;
    }
}
