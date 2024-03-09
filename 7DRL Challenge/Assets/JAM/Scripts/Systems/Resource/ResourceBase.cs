namespace JAM.Shared.Systems.Resource
{
    /// <summary>
    /// Base class for all resources.
    /// A Resource has a Name and description and it can be increased and decreased.
    /// </summary>
    public abstract class ResourceBase
    {
        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the description of the resource.
        /// </summary>
        public string Description { get; private set; }


        /// <summary>
        /// Initializes a new instance of the ResourceBase class.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="description">The description of the resource.</param>
        protected ResourceBase(string name, string description)
        {
            Name = name;
            Description = description;
        }


        /// <summary>
        /// Returns a string representation of the resource.
        /// </summary>
        /// <returns>A string representing the resource.</returns>
        public override string ToString()
        {
            return $"{this} => {Name}. {Description}";
        }


        /// <summary>
        /// Increases the resource by the specified amount.
        /// </summary>
        /// <param name="amountToIncrease">The amount to increase the resource by.</param>
        protected abstract void IncreaseResource(float amountToIncrease);
        /// <summary>
        /// Decreases the resource by the specified amount.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease the resource by.</param>
        protected abstract void DecreaseResource(float amountToDecrease);
    }
}
