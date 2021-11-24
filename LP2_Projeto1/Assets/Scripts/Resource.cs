using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : IResource
{
    public ResourceType Type { get; set; }
    public int GoldValue{get; set;}
    public int FoodValue{get; set;}

    public Resource(ResourceType type)
    {
        Type = type;

        switch (type)
        {
            case ResourceType.Plants:
                GoldValue = 1;
                FoodValue = 2;
                break;
            
            case ResourceType.Animals:
                GoldValue = 2;
                FoodValue = 3;
                break;
            
            case ResourceType.Metals:
                GoldValue = 3;
                FoodValue = -1;
                break;
            
            case ResourceType.FossilFuel:
                GoldValue = 4;
                FoodValue = -2;
                break;
            
            case ResourceType.Luxury:
                GoldValue = 4;
                FoodValue = 0;
                break;
            
            case ResourceType.Pollution:
                GoldValue = -2;
                FoodValue = -3;
                break;
        }
    }
}
