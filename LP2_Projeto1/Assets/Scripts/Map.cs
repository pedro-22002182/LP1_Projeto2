using UnityEngine;

/// <summary>
/// The <c>Map</c> class.
/// Has all the content of a map created with the information given by the file
/// loaded by the user.
/// </summary>
public class Map : MonoBehaviour
{  
    
      // //////////////////////
     //   Class variable   //
    // /////////////////////
    
    /// <summary>
    /// Array that contains all the potential resources a tile can have.
    /// </summary>
    /// <value>Available resources.</value>
    public static readonly string[] AvailableResources =
        {"plants","animals","metals","fossilfuel","luxury","pollution"};

      // //////////////////////
     //     Properties      //
    // ////////////////////// 
    
    /// <summary>
    /// Returns the number of rows in the map.
    /// </summary>
    /// <value>Amount of rows of the map.</value>
    public int Rows { get => _mapSize.rows; }

    /// <summary>
    /// Returns the number of columns in the map.
    /// </summary>
    /// <value>Amount of columns of the map.</value>
    public int Cols { get => _mapSize.cols; }

    // ///////////////////////
    // Instance variables  //
    // /////////////////////
    
    /// <summary>
    /// Tuple that represents the map size. It contains the amount of rows and
    /// columns of the map.
    /// </summary>
    private (int rows, int cols) _mapSize;

    /// <summary>
    /// Represents the map.
    /// </summary>
    private Tile[,] _tiles;

    /// <summary>
    /// Places a tile on the specified position.
    /// </summary>
    /// <param name="row">Row where the tile will be placed.</param>
    /// <param name="col">Column where the tile will be placed.</param>
    /// <param name="tile">The tile to be placed.</param>
    public void SetTile(int row, int col, Tile tile)
    {
        _tiles[row, col] = tile; 
    }

    /// <summary>
    /// Used to get information about a tile.
    /// </summary>
    /// <param name="row">Row where the tile to get information from is.</param>
    /// <param name="col">Column where the tile to get information from is.</param>
    /// <returns>Tile information.</returns>
    public Tile GetTile(int row, int col)
    {
        return _tiles[row, col];
    }

    /// <summary>
    /// Sets the size of the map.
    /// </summary>
    /// <param name="mapSize">
    /// Tuple containing the amount of rows and columns the map will have.
    /// </param>
    public void SetSize((int cols, int lines) mapSize)
    {
        _mapSize = mapSize;
        _tiles = new Tile[Rows, Cols];
    }
}
