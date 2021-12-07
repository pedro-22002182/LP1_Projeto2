/// <summary>
/// Interface that defines the properties that a resource must have.
/// </summary>
public interface IResource 
{
    /// <summary>
    /// Returns the type of resource.
    /// </summary>
    /// <value>Type of resource.</value>
    ResourceType Type { get; }

    /// <summary>
    /// Returns the additional amount of gold provided by the resource.
    /// </summary>
    /// <value>Amount of extra given gold.</value>
    int GoldValue { get; }

    /// <summary>
    /// Returns the additional amount of food provided by the resource.
    /// </summary>
    /// <value>Amount of extra given food.</value>
    int FoodValue { get; }
}
