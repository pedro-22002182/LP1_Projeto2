using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>Resource</c> class.
/// It represents a resource, holding all its information.
/// Resources may or may not be present in tiles. When they are, they give
/// bonuses to the tiles' base production values.
/// </summary>
public class Resource : IResource
{
    // ///////////////////
    // Class variables //
    // /////////////////

    /// <summary>
    /// A dictionary containing the key-value pairs of the possible resources
    /// contained in the file loaded by the user and their correspondant 
    /// resource type.
    /// </summary>
    /// <typeparam name="string">Resource present in the loaded file.</typeparam>
    /// <typeparam name="ResourceType">The <c>ResourceType</c>.</typeparam>
    /// <returns></returns>
    private static readonly IDictionary<string, ResourceType> resourceDict = 
        new Dictionary<string, ResourceType>()
    {
        {"plants", ResourceType.Plants},
        {"animals", ResourceType.Animals},
        {"metals", ResourceType.Metals},
        {"fossilfuel", ResourceType.FossilFuel},
        {"luxury", ResourceType.Luxury},
        {"pollution", ResourceType.Pollution}
    };

    // ///////////////////////
    // Instance variables  //
    // /////////////////////

    /// <summary>The type of resource.</summary>
    private readonly ResourceType _type;

    /// <summary>Extra gold given by the resource.</summary>
    private int _goldValue;

    /// <summary>Extra food given by the resource.</summary>
    private int _foodValue;

    /// <summary>Color that visually represents the type of resource.</summary>
    private Color _color;

    // //////////////
    // Properties //
    // ////////////

    /// <summary>
    /// Returns the color that visually represents the resource.
    /// </summary>
    /// <value>Resource's color representation.</value>
    public Color Color { get => _color; }

    /// <summary>
    /// Returns the type of the resource.
    /// </summary>
    /// <value>Resource's type.</value>
    public ResourceType Type { get => _type; }

    /// <summary>
    /// Returns the gold production bonus given by the resource.
    /// </summary>
    /// <value>Gold production bonus.</value>
    public int GoldValue{ get => _goldValue; }

    /// <summary>
    /// Returns the food production bonus given by the resource.
    /// </summary>
    /// <value>Food production bonus.</value>
    public int FoodValue{ get => _foodValue; }

    /// <summary>
    /// The constructor of the <c>Resource</c> class.
    /// It creates a new instance of <c>Resource</c>.
    /// </summary>
    /// <param name="type">The type of resource read on the loaded file.</param>
    public Resource(string type)
    {
        resourceDict.TryGetValue(type, out _type);
        DefineValues();
    }

    /// <summary>
    /// Initializes all the instance variables of the <c>Resource</c> class.
    /// </summary>
    private void DefineValues()
    {
        // The resource type will define the values of the given bonuses and
        // the color that visually represents the resource.
        switch (Type)
        {
            case ResourceType.Plants:
                    _goldValue = 1;
                    _foodValue = 2;
                    _color = new Color(0.290f, 0.647f, 0.396f);
                    break;
                
            case ResourceType.Animals:
                _goldValue = 2;
                _foodValue = 3;
                _color = new Color(0.721f, 0.258f, 0.274f);
                break;
            
            case ResourceType.Metals:
                _goldValue = 3;
                _foodValue = -1;
                _color = new Color(0.341f, 0.341f, 0.341f);
                break;
            
            case ResourceType.FossilFuel:
                _goldValue = 4;
                _foodValue = -2;
                _color = new Color(0.878f, 0.501f, 0.60f);
                break;
            
            case ResourceType.Luxury:
                _goldValue = 4;
                _foodValue = 0;
                _color = new Color(0.250f, 0.364f, 0.752f);
                break;
            
            case ResourceType.Pollution:
                _goldValue = -2;
                _foodValue = -3;
                _color = Color.black;
                break;
        }
    }
}
