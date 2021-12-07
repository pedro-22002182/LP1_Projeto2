using UnityEngine;

/// <summary>
/// ScriptableObject that contains a map's information to be shared by the
/// <c>Controller</c> and <c>PopulateGrid</c>.
/// </summary>
[CreateAssetMenu(menuName = "MapContainer")]
public class MapContainer : ScriptableObject
{
    /// <summary>
    /// An instance of the <c>Map</c> class.
    /// </summary>
    /// <value>Mao information.</value>
    public Map Map { get; private set; }

    /// <summary>
    /// Initializes the map.
    /// </summary>
    private void OnEnable()
    {
        Map = new Map();
    }
}
