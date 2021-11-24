using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private Tile[,] tiles;
    public int NLines {get;}
    public int NColumns {get;}

    public Map(int lines, int columns)
    {
        NLines = lines;
        NColumns = columns;

        tiles = new Tile[NLines, NColumns];
    }

    public void SetBuildMap(Tile tile, int line, int collumn)
    {
        tiles[line, collumn] = tile;
    }
}
