using NaughtyAttributes;
using System;
using UnityEngine;

namespace JAM.Shared.Systems.Resource
{
    /// <summary>
    /// Example client script testing a Player Health
    /// </summary>
    public class ResourceSystemClient : MonoBehaviour
    {
        [SerializeField] private IResourceSystem _entityHealthSystem;

        public event Action<float> EmptyHealth;

        private void Start()
        {
            TryGetComponent(out _entityHealthSystem);
            _entityHealthSystem.AmountEmptied.AddListener(x => EmptyHealth?.Invoke(x));
        }


        internal void HealPlayer(HealData data)
        {
            // The player only heals by a fixed amount
            _entityHealthSystem.IncreaseAmount(data.Amount);
        }

        internal void TakeDamagePlayer(DamageData data)
        {
            // The player only get hurt by a fixed amount
            _entityHealthSystem.DecreaseAmount(data.Amount);
        }


        #region Debug
        [Button]
        /// <summary>
        /// Debug method
        /// </summary>
        public void Heal()
        {
            var data = new HealData
            {
                Amount = 1f
            };
            HealPlayer(data);
        }

        [Button]
        /// <summary>
        /// Debug method
        /// </summary>
        public void TakeDamage()
        {
            var data = new DamageData
            {
                Amount = 1f
            };
            TakeDamagePlayer(data);
        }
        #endregion
    }
}
