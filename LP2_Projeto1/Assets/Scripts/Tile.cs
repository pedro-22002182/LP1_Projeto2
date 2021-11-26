using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private static readonly IDictionary<string, TerrainType> terrainDict = 
            new Dictionary<string, TerrainType>()
        {
            {"desert", TerrainType.Desert},
            {"grassland", TerrainType.Grassland},
            {"hills", TerrainType.Hills},
            {"mountain", TerrainType.Mountain},
            {"ocean", TerrainType.Ocean}
        };

    private ICollection<Resource> _resources;
    private readonly TerrainType _terrainType;
    private int baseGoldValue;
    private int baseFoodValue;

    public TerrainType Terrain { get => _terrainType; }
    
    public int GoldProduced
    {
        get
        {
            int goldProduction = baseGoldValue;

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

    public int FoodProduced
    {
        get
        {
           int foodProduction = baseFoodValue;

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

    public Tile(string terrain, ICollection<Resource> resources)
    {
        terrainDict.TryGetValue(terrain, out _terrainType);
        DefineBaseValues();
        _resources = new List<Resource>();
        DefineResources(resources);
    }

    private void DefineBaseValues()
    {
        switch (Terrain)
        {
            case TerrainType.Desert:
                baseGoldValue = 0;
                baseFoodValue = 0;
                break;
            
            case TerrainType.Grassland:
                baseGoldValue = 0;
                baseFoodValue = 2;
                break;
            
            case TerrainType.Hills:
                baseGoldValue = 1;
                baseFoodValue = 1;
                break;
            
            case TerrainType.Mountain:
                baseGoldValue = 1;
                baseFoodValue = 0;
                break;

            case TerrainType.Ocean:
                baseGoldValue = 0;
                baseFoodValue = 1;
                break;
        }
    }

    public void DefineResources(ICollection<Resource> resources)
    {
        foreach (Resource r in resources)
            _resources.Add(r);
    }
}
