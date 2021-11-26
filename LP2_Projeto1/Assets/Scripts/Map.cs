using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int Rows { get => _mapSize.rows; }
    public int Cols { get => _mapSize.cols; }

    private (int rows, int cols) _mapSize;
    private Tile[,] _tiles;

    public Map((int cols, int lines) mapSize)
    {
        _mapSize = mapSize;
        _tiles = new Tile[Rows, Cols];
    }

    public void SetTile(int row, int col, Tile tile)
    {
        _tiles[row, col] = tile; 
    }

    public Tile GetTile(int row, int col)
    {
        return _tiles[row, col];
    }
}
