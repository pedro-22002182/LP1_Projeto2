using System.Collections.Generic;

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
                    break;
                
            case ResourceType.Animals:
                _goldValue = 2;
                _foodValue = 3;
                break;
            
            case ResourceType.Metals:
                _goldValue = 3;
                _foodValue = -1;
                break;
            
            case ResourceType.FossilFuel:
                _goldValue = 4;
                _foodValue = -2;
                break;
            
            case ResourceType.Luxury:
                _goldValue = 4;
                _foodValue = 0;
                break;
            
            case ResourceType.Pollution:
                _goldValue = -2;
                _foodValue = -3;
                break;
        }
    }
}
