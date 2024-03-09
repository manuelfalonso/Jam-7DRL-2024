using System;

namespace JAM.Interfaces
{
    public interface IDamageable 
    {
        Action<DamageData> Damaged { get; set; }

        bool TryTakeDamage(DamageData data);
    }
}
