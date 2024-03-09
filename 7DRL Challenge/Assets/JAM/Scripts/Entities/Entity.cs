using JAM.Interfaces;
using JAM.Shared.Systems.Resource;
using System;
using JAM.Stats;
using UnityEngine;

namespace JAM.Entities
{
    public class Entity : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] protected ResourceSystemClient _healthSystem;
        [SerializeField] protected StatSheet _stats;
        [SerializeField] protected bool _chasing;

        public StatContainer StatContainer { get; protected set; }
        public Action<DamageData> Damaged { get; set; }

        public ResourceSystemClient HealthSystem => _healthSystem;
        public bool IsChasing() => _chasing;


        protected virtual void Awake()
        {
            StatContainer = new StatContainer(_stats);
            _healthSystem.EmptyHealth -= DeathOfEntity;
            _healthSystem.EmptyHealth += DeathOfEntity;
        }


        #region Interfaces
        public bool TryHeal(HealData data)
        {
            if (data == null) { return false; }
            if (_healthSystem == null) { return false; }

            _healthSystem.HealPlayer(data);

            return true;
        }

        public bool TryTakeDamage(DamageData data)
        {
            if (data == null) { return false; }
            if (_healthSystem == null) { return false; }

            _healthSystem.TakeDamagePlayer(data);
            Damaged?.Invoke(data);

            return true;
        }

        protected virtual void DeathOfEntity(float toCall) 
        {
        }
        #endregion
    }
}
