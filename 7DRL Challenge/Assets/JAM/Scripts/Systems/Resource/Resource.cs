using System;

namespace JAM.Shared.Systems.Resource
{
    /// <summary>
    /// Serializable class representing a resource with an amount, maximum amount, and initial amount.
    /// </summary>
    [Serializable]
    public class Resource : ResourceBase
    {
        /// <summary>
        /// Gets the current amount of the resource.
        /// </summary>
        public float Amount { get; private set; }
        /// <summary>
        /// Gets the maximum amount that the resource can have.
        /// </summary>
        public float MaxAmount { get; private set; }
        /// <summary>
        /// Gets the initial amount of the resource.
        /// </summary>
        public float InitialAmount { get; private set; }
        /// <summary>
        /// Gets the normalized amount of the resource (amount / maxAmount).
        /// </summary>
        public float AmountNormalized => Amount / MaxAmount;


        /// <summary>
        /// Creates a new instance of the Resource class.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="description">The description of the resource.</param>
        /// <param name="initialAmount">The initial amount of the resource.</param>
        /// <param name="maxAmount">The maximum amount the resource can have.</param>
        public Resource(string name, string description, float initialAmount, float maxAmount) : base(name, description)
        {
            // Initial Resource Amount validation
            if (initialAmount > maxAmount || initialAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialAmount), InitialAmountExceptionMessage());
            }

            Amount = initialAmount;
            InitialAmount = initialAmount;
            MaxAmount = maxAmount;
        }


        /// <summary>
        /// Increases the resource amount by the specified value.
        /// </summary>
        /// <param name="amountToIncrease">The amount to increase.</param>
        public void IncreaseAmount(float amountToIncrease)
        {
            IncreaseResource(amountToIncrease);
        }

        /// <summary>
        /// Decreases the resource amount by the specified value.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease.</param>
        public void DecreaseAmount(float amountToDecrease)
        {
            DecreaseResource(amountToDecrease);
        }

        /// <summary>
        /// Clears the resource amount, setting it to zero.
        /// </summary>
        public void ClearAmount()
        {
            ClearResource();
        }

        /// <summary>
        /// Resets the resource amount to the initial amount.
        /// </summary>
        public void ResetAmount()
        {
            ResetResource();
        }


        /// <inheritdoc/>
        protected override void IncreaseResource(float amountToIncrease)
        {
            Amount += amountToIncrease;
            if (Amount > MaxAmount) { Amount = MaxAmount; }
        }

        /// <inheritdoc/>
        protected override void DecreaseResource(float amountToDecrease)
        {
            Amount -= amountToDecrease;
            if (Amount < 0) { Amount = 0; }
        }

        /// <summary>
        /// Resets the resource amount to the initial amount.
        /// </summary>
        protected virtual void ResetResource()
        {
            Amount = InitialAmount;
        }

        /// <summary>
        /// Clears the resource amount, setting it to zero.
        /// </summary>
        protected virtual void ClearResource()
        {
            Amount = 0;
        }


        /// <summary>
        /// Generates the exception message for the initial amount validation.
        /// </summary>
        /// <returns>The exception message.</returns>
        private string InitialAmountExceptionMessage()
        {
            return $"initialAmount can't be larger than maxAmount or less than minAmount";
        }
    }
}
