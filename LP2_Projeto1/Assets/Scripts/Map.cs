using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private readonly int[,] map;

    public int this[int row, int col]
    {
        get => map[row, col];
        set => map[row, col] = value;
    }

    public int Rows => map.GetLength(0);
    public int Cols => map.GetLength(1);

    public Map(int rows, int cols)
    {
        map = new int[rows, cols];
    }
}
