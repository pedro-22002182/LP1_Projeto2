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
    private TerrainType _terrainType;
    private int _baseGoldValue;
    private int _baseFoodValue;
    private Color _color;

    public Color Color { get => _color; }
    public TerrainType Terrain { get => _terrainType; }
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

    public Tile(string terrain, ICollection<Resource> resources)
    {
        terrainDict.TryGetValue(terrain, out _terrainType);
        DefineBaseValues();
        _resources = new List<Resource>();
        DefineResources(resources);
    }

    public void ChangeTile(string terrain, ICollection<Resource> resources)
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

    public void DefineResources(ICollection<Resource> resources)
    {
        foreach (Resource r in resources)
            _resources.Add(r);
    }
}
