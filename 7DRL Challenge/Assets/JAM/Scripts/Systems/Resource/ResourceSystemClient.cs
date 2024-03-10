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
        [SerializeField] private bool _useView;
        [SerializeField] private ResourceSystemView _resourceSystemView;

        private IResourceSystem _entityHealthSystem;

        public event Action<float> EmptyHealth;


        private void Start()
        {
            TryGetComponent(out _entityHealthSystem);
            _entityHealthSystem.AmountEmptied.AddListener(x => EmptyHealth?.Invoke(x));
            InitializeResourceSystemView();
        }


        private void InitializeResourceSystemView()
        {
            if(!_useView) {return;}
            _resourceSystemView.MinLife = 0f;
            _resourceSystemView.MaxLife = _entityHealthSystem.MaxAmount;
            _resourceSystemView.UpdateHealthBar(_entityHealthSystem.Amount);
            _entityHealthSystem.AmountChanged.AddListener(_resourceSystemView.UpdateHealthBar);
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

        internal void RestorePlayer()
        {
            // Restore Player health to the initial amount
            _entityHealthSystem.ResetAmount();
        }


        #region Debug
        [Button]
        /// <summary>
        /// Debug method to heal the player
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
        /// Debug method to damage the player
        /// </summary>
        public void TakeDamage()
        {
            var data = new DamageData
            {
                Amount = 1f
            };
            TakeDamagePlayer(data);
        }

        [Button]
        /// <summary>
        /// Debug method to restore player health
        /// </summary>
        public void Revive()
        {
            RestorePlayer();
        }
        #endregion


        #region OnGUI
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 150, 100), "Heal Player"))
            {
                Heal();
            }

            if (GUI.Button(new Rect(10, 120, 150, 100), "Damage Player"))
            {
                TakeDamage();
            }

            if (GUI.Button(new Rect(10, 230, 150, 100), "Revive Player"))
            {
                Revive();
            }
        }
        #endregion
    }
}
