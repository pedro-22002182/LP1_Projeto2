using System.Collections.Generic;
using UnityEngine;

public class Resource : IResource
{
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

    private readonly ResourceType _type;
    private int _goldValue;
    private int _foodValue;
    private Color _color;

    public Color Color { get => _color; }
    public ResourceType Type { get => _type; }
    public int GoldValue{ get => _goldValue; }
    public int FoodValue{ get => _foodValue; }

    public Resource(string type)
    {
        resourceDict.TryGetValue(type, out _type);
        DefineValues();
    }

    private void DefineValues()
    {
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
