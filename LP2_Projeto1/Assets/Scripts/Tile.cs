using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private ICollection<IResource> resources;
    private int baseGoldValue;
    private int baseFoodValue;

    public TerrainType Terrain { get; }
    
    public int GoldProduced
    {
        get
        {
            int goldProduction = baseGoldValue;

            foreach (IResource r in resources)
            {
                goldProduction =+ r.GoldValue;
            }

            return goldProduction;
        }
    }

    public int FoodProduced
    {
        get
        {
           int foodProduction = baseFoodValue;

            foreach (IResource r in resources)
            {
                foodProduction =+ r.FoodValue;
            }

            return foodProduction; 
        }
    }

    public Tile(TerrainType terrain, ICollection<IResource> resources)
    {
        Terrain = terrain;
        this.resources = resources;
        DefineBaseValues();
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
}
