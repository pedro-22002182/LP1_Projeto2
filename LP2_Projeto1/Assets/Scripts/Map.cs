using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Tile[,] tiles;
    private int nLines;
    private int nColumns;

    public Map(int lines, int columns)
    {
        nLines = lines;
        nColumns = columns;

        tiles = new Tile[nLines, nColumns];
    }

    public void SetBuildMap(Tile tile, int line, int collumn)
    {
        tiles[line, collumn] = tile;
    }
}
