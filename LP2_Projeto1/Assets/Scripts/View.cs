using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class View : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private GridLayoutGroup gridLayout;

    private Map map;

    // Start is called before the first frame update
    void Start()
    {
        int collumns = map.Cols;
        int lines = map.Rows;

        SetGridLayout(lines, collumns);
        BuildViewMap(lines, collumns);
        

        Destroy(tile);
    }

    private void SetGridLayout(int lines, int collumns)
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = collumns;

        gridLayout.cellSize = new Vector2(800 / collumns, 600 / lines);
    }

    private void BuildViewMap(int lines, int collumns)
    {
        for(int i = 0; i < lines; i++)
        {
            for(int y = 0; y < collumns; y++)
            {
                GameObject newTile = Instantiate(tile, transform);
                newTile.transform.parent = tile.transform.parent;

                newTile.transform.GetChild(0).GetComponent<TextMeshPro>().text = newTile.GetComponent<Tile>().GoldProduced.ToString();
                newTile.transform.GetChild(1).GetComponent<TextMeshPro>().text = newTile.GetComponent<Tile>().FoodProduced.ToString();
            }
        }
    }


    //Ativar Detalhes Tile
    public void OpenTile()
    {
        
    }
}
