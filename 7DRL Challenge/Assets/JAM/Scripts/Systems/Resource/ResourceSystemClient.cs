using BossRushJam2024.Entities;
using NaughtyAttributes;
using UnityEngine;

namespace BossRushJam2024.Shared.Systems.Resource
{
    /// <summary>
    /// Example client script testing a Player Health
    /// </summary>
    public class ResourceSystemClient : MonoBehaviour
    {
        /// <summary>
        /// Amount to increase or decrease the health system by
        /// </summary>
        [Tooltip("Amount to increase or decrease the health system by")]
        [SerializeField] private float _increaseDecreaseAmount = 1f;

        private IResourceSystem _entityHealthSystem = null;


        private void Start()
        {
            if (TryGetComponent(out Entity entity))
            {
                _entityHealthSystem = entity.HealthSystem;
            }
        }


        internal void HealPlayer()
        {
            // The player only heals by a fixed amount
            _entityHealthSystem.IncreaseAmount(_increaseDecreaseAmount);
        }

        internal void TakeDamagePlayer()
        {
            // The player only get hurt by a fixed amount
            _entityHealthSystem.DecreaseAmount(_increaseDecreaseAmount);
        }


        #region Debug
        [Button]
        /// <summary>
        /// Debug method
        /// </summary>
        public void Heal()
        {
            HealPlayer();
        }

        [Button]
        /// <summary>
        /// Debug method
        /// </summary>
        public void TakeDamage()
        {
            TakeDamagePlayer();
        }
        #endregion







        //IEnumerator Start()
        //{
        //    // Life Resource example
        //    _playerHealthSystem.AmountEmptied.AddListener(PlayerHealthSystem_OnEmptyResource);
        //    _playerHealthSystem.AmountChanged.AddListener(PlayerHealthSystem_OnResourceChanged);
        //    _playerHealthSystem.AmountRestored.AddListener(PlayerHealthSystem_OnRestoreResource);
        //    _playerHealthSystem.AmountMaxed.AddListener(PlayerHealthSystem_OnMaxResource);
        //    _playerHealthSystem.AmountLow.AddListener(PlayerHealthSystem_OnLowResource);

        //    // Get data
        //    Log($"* Player Initial life: {_playerHealthSystem.Amount}", this);
        //    Log($"* Player Max life: {_playerHealthSystem.MaxAmount}", this);
        //    Log($"* Player has {_playerHealthSystem.ResourcePercent * 100f}% of life", this);

        //    // Methods
        //    Log($"* Damage Player", this);
        //    _playerHealthSystem.DecreaseAmountWithResult(80f);
        //    Log($"* Heal Player to max Health", this);
        //    _playerHealthSystem.IncreaseAmountWithResult(9999f);
        //    Log($"* Tree fall and killed the Player", this);
        //    _playerHealthSystem.ClearAmountWithResult();
        //    Log($"* Revive Player by half of this life", this);
        //    _playerHealthSystem.RestoreAmountWithResult(0.5f);
        //    Log($"* Player health now is immutable", this);
        //    _playerHealthSystem.Immutable = true;
        //    Log($"* Tring to damage the Player", this);
        //    _playerHealthSystem.DecreaseAmountWithResult(10f);
        //    Log($"* Player health now is not immutable", this);
        //    _playerHealthSystem.Immutable = false;
        //    Log($"* Killed the Player, AGAIN!", this);
        //    _playerHealthSystem.ClearAmountWithResult();
        //    Log($"* But now reset it to initial amount", this);
        //    _playerHealthSystem.ResetAmountWithResult();

        //    yield break;
        //}


        //private void PlayerHealthSystem_OnLowResource(float healthAmount)
        //{
        //    Log($"Player OnLowResource", this);
        //}

        //private void PlayerHealthSystem_OnMaxResource(float healthAmount)
        //{
        //    Log($"Player OnMaxResource", this);
        //}

        //private void PlayerHealthSystem_OnRestoreResource(float healthAmount)
        //{
        //    Log($"Player OnRestoreResource", this);
        //}

        //private void PlayerHealthSystem_OnResourceChanged(float healthAmount)
        //{
        //    Log($"Player OnResourceChanged: {_playerHealthSystem.Amount}", this);
        //}

        //private void PlayerHealthSystem_OnEmptyResource(float healthAmount)
        //{
        //    Log($"Player OnEmptyResource", this);
        //}


        //public void Log(object message, Object sender = null)
        //{
        //    if (sender != null)
        //        Debug.Log($"{this} => {message}", sender);
        //    else
        //        Debug.Log($"{this} => {message}");
        //}

    }
}
