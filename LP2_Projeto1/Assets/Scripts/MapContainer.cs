using UnityEngine;

[CreateAssetMenu(menuName = "MapContainer")]
public class MapContainer : ScriptableObject
{
    public TestMap Map { get; private set; }

    private void OnEnable()
    {
        Map = new TestMap();
    }
}