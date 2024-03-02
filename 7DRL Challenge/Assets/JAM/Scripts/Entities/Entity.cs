using BossRushJam2024.Interfaces;
using BossRushJam2024.Shared.Systems.Resource;
using System;
using Pampero.Stats;
using UnityEngine;

namespace BossRushJam2024.Entities
{
    public class Entity : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] protected ResourceSystem _healthSystem;
        [SerializeField] protected StatSheet _stats;
        [SerializeField] protected bool _chasing;
        public StatContainer StatContainer { get; protected set; }

        public ResourceSystem HealthSystem { get => _healthSystem; private set => _healthSystem = value; }
        public Action<DamageData> Damaged { get; set; }

        protected virtual void Awake()
        {
            StatContainer = new StatContainer(_stats);
            _healthSystem._amountEmptied.AddListener(DeathOfEntity);
        }

        public bool IsChasing() => _chasing;

        #region Interfaces
        public bool TryHeal(HealData data)
        {
            if (data == null) { return false; }
            if (_healthSystem == null) { return false; }

            _healthSystem.IncreaseAmount(data.Amount);

            return true;
        }

        public bool TryTakeDamage(DamageData data)
        {
            if (data == null) { return false; }
            if (_healthSystem == null) { return false; }

            _healthSystem.DecreaseAmount(data.Amount);
            Damaged?.Invoke(data);

            return true;
        }

        protected virtual void DeathOfEntity(float toCall) 
        {
        }

        #endregion
    }
}
