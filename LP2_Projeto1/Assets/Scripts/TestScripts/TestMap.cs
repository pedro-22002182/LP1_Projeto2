using UnityEngine;

public class TestMap : MonoBehaviour
{
    public static readonly string[] AvailableResources = {"plants","animals","metals","fossilfuel","luxury","pollution"};
    public int Rows { get => _mapSize.rows; }
    public int Cols { get => _mapSize.cols; }

    private (int rows, int cols) _mapSize;
    private Tile[,] _tiles;

    public void SetTile(int row, int col, Tile tile)
    {
        _tiles[row, col] = tile; 
    }

    public Tile GetTile(int row, int col)
    {
        return _tiles[row, col];
    }

    public void SetSize((int cols, int lines) mapSize)
    {
        _mapSize = mapSize;
        _tiles = new Tile[Rows, Cols];
    }
}
