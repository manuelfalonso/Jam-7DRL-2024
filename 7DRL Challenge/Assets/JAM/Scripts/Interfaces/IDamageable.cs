using System;

namespace BossRushJam2024.Interfaces
{
    public interface IDamageable 
    {
        Action<DamageData> Damaged { get; set; }

        bool TryTakeDamage(DamageData data);
    }
}
