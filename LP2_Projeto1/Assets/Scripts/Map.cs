using UnityEngine;

public class Map : MonoBehaviour
{  
    
     // ///////////////////////
    //   Class variable   //
    // /////////////////////
    
    /// <summary>
    /// Represents all potencial resources.
    /// </summary>
    /// <value>Available resources</value>
    public static readonly string[] AvailableResources = {"plants","animals","metals","fossilfuel","luxury","pollution"};

      // ///////////////////////
        // Properties //
    // ///////////////////// 
    
    /// <summary>
    /// Returns the number of rows in map.
    /// </summary>
    /// <value>Rows in map</value>
    public int Rows { get => _mapSize.rows; }

    /// <summary>
    /// Returns the number of colums in map.
    /// </summary>
    /// <value>Colums in map</value>
    public int Cols { get => _mapSize.cols; }

    // ///////////////////////
    // Instance variables  //
    // /////////////////////
    
    /// <summary>
    /// Takes rows and cols value to mapSize.
    /// </summary>
    private (int rows, int cols) _mapSize;

    /// <summary>
    /// Represents map
    /// </summary>
    private Tile[,] _tiles;

    /// <summary>
    /// Set a tile in each position based on the size of map(rows,cols).
    /// </summary>
    /// <param name="row">Map rows</param>
    /// <param name="col">Map Colums</param>
    /// <param name="tile">One specific tile</param>
    public void SetTile(int row, int col, Tile tile)
    {
        _tiles[row, col] = tile; 
    }

    /// <summary>
    /// Used to get information about a tile
    /// </summary>
    /// <param name="row">Map rows</param>
    /// <param name="col">Map Colums</param>
    /// <returns>Tile information</returns>
    public Tile GetTile(int row, int col)
    {
        return _tiles[row, col];
    }

    /// <summary>
    /// Sets the size of map and the number os tiles based on the size
    /// </summary>
    /// <param name="cols">Map Colums</param>
    /// <param name="mapSize">Size of map</param>
    public void SetSize((int cols, int lines) mapSize)
    {
        _mapSize = mapSize;
        _tiles = new Tile[Rows, Cols];
    }
}
