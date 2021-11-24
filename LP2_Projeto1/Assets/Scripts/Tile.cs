using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private ICollection<IResource> resources;
    private int baseGoldValue;
    private int baseFoodValue;

    public TerrainType Terrain { get; }
    public int GoldProduced { get; }
    public int FoodProduced { get; }
}
