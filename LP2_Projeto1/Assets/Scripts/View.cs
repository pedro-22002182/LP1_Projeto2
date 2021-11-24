using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private GridLayoutGroup gridLayout;

    private Map map;

    // Start is called before the first frame update
    void Start()
    {
        int collumns = map.NColumns;
        int lines = map.NLines;

        SetGridLayout(lines, collumns);
        BuildMap(lines, collumns);
        

        Destroy(tile);
    }

    private void SetGridLayout(int lines, int collumns)
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = collumns;

        gridLayout.cellSize = new Vector2(800 / collumns, 600 / lines);
    }

    private void BuildMap(int lines, int collumns)
    {
        for(int i = 0; i < lines; i++)
        {
            for(int y = 0; y < collumns; y++)
            {
                GameObject newTile = Instantiate(tile, transform);
                newTile.transform.parent = tile.transform.parent;
            }
        }
    }
}
