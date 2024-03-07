namespace JAM.Shared.Systems.Resource
{
    /// <summary>
    /// Represents data related to a resource system.
    /// </summary>
    public class ResourceSystemData
    {
        /// <summary>
        /// Gets or sets the current amount of the resource.
        /// </summary>
        public float Amount;
        /// <summary>
        /// Gets or sets the maximum amount of the resource.
        /// </summary>
        public float MaxAmount;
        /// <summary>
        /// Gets or sets the percentage of the resource relative to its maximum amount.
        /// </summary>
        public float Percent;
        /// <summary>
        /// Gets or sets a value indicating whether the resource is at its maximum amount.
        /// </summary>
        public bool IsMaxAmount;
        /// <summary>
        /// Gets or sets a value indicating whether the resource is at a low amount.
        /// </summary>
        public bool IsLowAmount;
        /// <summary>
        /// Gets or sets a value indicating whether the resource is empty.
        /// </summary>
        public bool IsEmptyAmount;
        /// <summary>
        /// Gets or sets a value indicating whether the resource is immutable.
        /// </summary>
        public bool Immutable;
        /// <summary>
        /// Gets or sets a value indicating whether the resource is currently in cooldown.
        /// </summary>
        public bool IsInCooldown;
    }
}
