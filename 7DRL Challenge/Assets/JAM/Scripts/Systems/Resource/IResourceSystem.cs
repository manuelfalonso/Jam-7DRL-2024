using UnityEngine.Events;

namespace JAM.Shared.Systems.Resource
{
    public interface IResourceSystem
    {
        /// <summary>
        /// Gets the current resource amount.
        /// </summary>
        float Amount { get; }
        /// <summary>
        /// Gets the maximum resource amount.
        /// </summary>
        float MaxAmount { get; }
        /// <summary>
        /// Gets or sets the low amount threshold.
        /// </summary>
        float LowAmount { get; set; }
        /// <summary>
        /// Gets the resource percentage.
        /// </summary>
        float ResourcePercent { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource is immutable.
        /// </summary>
        bool Immutable { get; set; }
        /// <summary>
        /// Gets a value indicating whether the resource is empty.
        /// </summary>
        bool IsEmptyAmount { get; }
        /// <summary>
        /// Gets a value indicating whether the resource is in a low amount state.
        /// </summary>
        bool IsLowAmount { get; }
        /// <summary>
        /// Gets a value indicating whether the resource is at its maximum amount.
        /// </summary>
        bool IsMaxAmount { get; }

        /// <summary>
        /// Gets a value indicating whether the resource is in an increase cooldown state.
        /// </summary>
        bool IsInIncreaseCooldown { get; }
        /// <summary>
        /// Gets or sets the cooldown time for resource increase.
        /// </summary>
        float IncreaseCooldownTime { get; set; }
        /// <summary>
        /// Gets a value indicating whether the resource is in a decrease cooldown state.
        /// </summary>
        bool IsInDecreaseCooldown { get; }
        /// <summary>
        /// Gets or sets the cooldown time for resource decrease.
        /// </summary>
        float DecreaseCooldownTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource regenerates.
        /// </summary>
        bool Regenerate { get; set; }
        /// <summary>
        /// Gets or sets the regeneration tick time in seconds.
        /// </summary>
        float RegenerationTick { get; set; }
        /// <summary>
        /// Gets or sets the regeneration rate per tick.
        /// </summary>
        float RegenerationRate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the resource degenerates.
        /// </summary>
        bool Degenerate { get; set; }
        /// <summary>
        /// Gets or sets the degeneration tick time in seconds.
        /// </summary>
        float DegenerationTick { get; set; }
        /// <summary>
        /// Gets or sets the degeneration rate per tick.
        /// </summary>
        float DegenerationRate { get; set; }

        /// <summary>
        /// Event invoked when the resource amount changes.
        /// </summary>
        public UnityEvent<float> AmountChanged { get; set; }
        /// <summary>
        /// Event invoked when the resource amount changes. Returns the normalized amount.
        /// </summary>
        public UnityEvent<float> AmountChangedNormalized { get; set; }
        /// <summary>
        /// Event invoked when the resource amount is restored.
        /// </summary>
        public UnityEvent<float> AmountRestored { get; set; }
        /// <summary>
        /// Event invoked when the resource amount reaches its maximum.
        /// </summary>
        public UnityEvent<float> AmountMaxed { get; set; }
        /// <summary>
        /// Event invoked when the resource amount is low.
        /// </summary>
        public UnityEvent<float> AmountLow { get; set; }
        /// <summary>
        /// Event invoked when the resource amount is emptied.
        /// </summary>
        public UnityEvent<float> AmountEmptied { get; set; }

        /// <summary>
        /// Increases the resource amount.
        /// The Resource must not be empty. Call RestoreAmount or ResetAmount instead
        /// </summary>
        /// <param name="amountToIncrease">Must be greater than 0</param>
        void IncreaseAmount(float amountToIncrease);
        /// <summary>
        /// Increases the resource amount and returns the result.
        /// The Resource must not be empty. Call RestoreAmount or ResetAmount instead
        /// </summary>
        /// <param name="amountToIncrease">Must be greater than 0</param>
        ResourceSystemResult IncreaseAmountWithResult(float amountToIncrease);
        /// <summary>
        /// Decreases the resource amount with the specified result.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease. Must be greater than 0</param>
        void DecreaseAmount(float amountToDecrease);
        /// <summary>
        /// Decreases the resource amount with the specified result.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease. Must be greater than 0</param>
        /// <returns>The result of the operation.</returns>
        ResourceSystemResult DecreaseAmountWithResult(float amountToDecrease);
        /// <summary>
        /// Restores resource amount only if the resource was empty.
        /// </summary>
        /// <param name="restorePercentage">The percentage to restore.</param>
        void RestoreAmount(float restorePercentage = 1);
        /// <summary>
        /// Restores resource amount only if the resource was empty.
        /// </summary>
        /// <param name="restorePercentage">The percentage to restore.</param>
        /// <returns>The result of the operation.</returns>
        ResourceSystemResult RestoreAmountWithResult(float restorePercentage = 1);
        /// <summary>
        /// Reset Resource to initial amount only if the resource was empty.
        /// </summary>
        void ResetAmount();
        /// <summary>
        /// Reset Resource to initial amount only if the resource was empty.
        /// </summary>
        /// <returns>The result of the operation.</returns>
        ResourceSystemResult ResetAmountWithResult();
        /// <summary>
        /// Clears the resource amount.
        /// </summary>
        void ClearAmount();
        /// <summary>
        /// Clears the resource amount.
        /// </summary>
        /// <returns>The result of the operation.</returns>
        ResourceSystemResult ClearAmountWithResult();
    }
}