using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The <c>Tile</c> class.
/// It represents a map tile, containing all its information.
/// </summary>
public class Tile : MonoBehaviour
{
    /// <summary>
    /// A dictionary containing the key-value pairs of the possible terrains
    /// contained in the file loaded by the user and their correspondant 
    /// terrain type.
    /// </summary>
    /// <typeparam name="string">Terrain present in the loaded file.</typeparam>
    /// <typeparam name="TerrainType">The <c>TerrainType</c>.</typeparam>
    /// <returns></returns>
    private static readonly IDictionary<string, TerrainType> terrainDict = 
            new Dictionary<string, TerrainType>()
        {
            {"desert", TerrainType.Desert},
            {"grassland", TerrainType.Grassland},
            {"hills", TerrainType.Hills},
            {"mountain", TerrainType.Mountain},
            {"ocean", TerrainType.Ocean}
        };

    /// <summary>
    /// Collection that contains the tile's resources.
    /// </summary>
    private ICollection<Resource> _resources;

    /// <summary>
    /// The tile's terrain type.
    /// </summary>
    private TerrainType _terrainType;

    /// <summary>
    /// The based value of gold produced by the tile.
    /// </summary>
    private int _baseGoldValue;

    /// <summary>
    /// The based value of food produced by the tile.
    /// </summary>
    private int _baseFoodValue;

    /// <summary>
    /// The tile's color representation.
    /// </summary>
    private Color _color;

    /// <summary>
    /// Returns the tile's color representation.
    /// </summary>
    /// <value>The tile's color representation.</value>
    public Color Color { get => _color; }

    /// <summary>
    /// Returns the tile's terrain type.
    /// </summary>
    /// <value>The tile's terrain type.</value>
    public TerrainType Terrain { get => _terrainType; }

    /// <summary>
    /// Returns the tile's resources.
    /// </summary>
    /// <value>The tile's resources.</value>
    public List<Resource> Resources
    {
        get
        {
            List<Resource> aux = new List<Resource>();

            foreach (Resource r in _resources)
                aux.Add(r);
            
            return aux;
        }
    }
    
    /// <summary>
    /// Returns the total amount of gold produced by the tile, which is equal to
    /// the base gold value plus the bonuses given by each resource the tile has.
    /// </summary>
    /// <value>The total amount of gold produced by the tile.</value>
    public int GoldProduced
    {
        get
        {
            int goldProduction = _baseGoldValue;

                if (_resources.Count > 0)
                {
                    foreach (Resource r in _resources)
                    {
                        goldProduction += r.GoldValue;
                    }
                }

                return goldProduction;
        }
    }

    /// <summary>
    /// Returns the total amount of food produced by the tile, which is equal to
    /// the base food value plus the bonuses given by each resource the tile has.
    /// </summary>
    /// <value>The total amount of food produced by the tile.</value>
    public int FoodProduced
    {
        get
        {
           int foodProduction = _baseFoodValue;

                if (_resources.Count > 0)
                {
                    foreach (Resource r in _resources)
                    {
                        foodProduction += r.FoodValue;
                    }
                }

                return foodProduction;
        }
    }

    /// <summary>
    /// The <c>Tile</c>'s constructor. Used to create a new instace of the
    /// <c>Tile</c> class.
    /// </summary>
    /// <param name="terrain">
    /// The terrain type of the tile that is going to be created.
    /// </param>
    /// <param name="resources">
    /// The resources of the tile that is going to be created.
    /// </param>
    public Tile(string terrain, ICollection<Resource> resources)
    {
        terrainDict.TryGetValue(terrain, out _terrainType);
        DefineBaseValues();
        _resources = new List<Resource>();
        DefineResources(resources);
    }

    /// <summary>
    /// Copies a <c>Tile</c>'s information into another <c>Tile</c>.
    /// </summary>
    /// <param name="terrain">
    /// The terrain of the <c>Tile</c> that is being copied.
    /// </param>
    /// <param name="resources">
    /// The resources of the <c>Tile</c> that is being copied.
    /// </param>
    public void ChangeTile(string terrain, ICollection<Resource> resources)
    {
        terrainDict.TryGetValue(terrain, out _terrainType);
        DefineBaseValues();
        _resources = new List<Resource>();
        DefineResources(resources);
    }

     /// <summary>
    /// Initializes all the instance variables of the <c>Tile</c> class.
    /// </summary>
    private void DefineBaseValues()
    {
        // The terrain type will define the values of the given bonuses and
        // the color that visually represents the terrain.
        switch (Terrain)
        {
            case TerrainType.Desert:
                _baseGoldValue = 0;
                _baseFoodValue = 0;
                _color = new Color(0.988f, 0.949f, 0.572f);
                break;
            
            case TerrainType.Grassland:
                _baseGoldValue = 0;
                _baseFoodValue = 2;
                _color = new Color(0.572f, 0.988f, 0.662f);
                break;
            
            case TerrainType.Hills:
                _baseGoldValue = 1;
                _baseFoodValue = 1;
                _color = new Color(0.760f, 0.756f, 0.741f);
                break;
            
            case TerrainType.Mountain:
                _baseGoldValue = 1;
                _baseFoodValue = 0;
                _color = new Color(0.6f, 0.474f, 0.301f);
                break;

            case TerrainType.Ocean:
                _baseGoldValue = 0;
                _baseFoodValue = 1;
                _color = new Color(0.6f, 0.772f, 0.960f);
                break;
        }
    }

    /// <summary>
    /// Adds resources the collection that contains the tile's resources.
    /// </summary>
    /// <param name="resources">Resources in tile's resources collection.</param>
    public void DefineResources(ICollection<Resource> resources)
    {
        foreach (Resource r in resources)
            _resources.Add(r);
    }
}
