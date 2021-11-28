using UnityEngine;

[CreateAssetMenu(menuName = "MapContainer")]
public class MapContainer : ScriptableObject
{
    public Map Map { get; private set; }

    private void OnEnable()
    {
        Map = new Map();
    }
}
