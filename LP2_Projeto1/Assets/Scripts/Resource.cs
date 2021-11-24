using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : IResource
{
    private ResourceType type;
    public int GoldValue{get; set;}
    public int FoodValue{get; set;}
}
