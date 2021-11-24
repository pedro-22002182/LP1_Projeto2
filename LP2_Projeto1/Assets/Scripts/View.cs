using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class View : MonoBehaviour
{
    [SerializeField] private GameObject tile, detailTile;
    [SerializeField] private GridLayoutGroup gridLayout;

    private Map map;

    // Start is called before the first frame update
    void Start()
    {
        int collumns = map.NColumns;
        int lines = map.NLines;

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
                GameObject newTileGameObject = Instantiate(tile, transform);
                newTileGameObject.transform.parent = tile.transform.parent;
                
                newTileGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = map.GetTile(i,y).GoldProduced.ToString();
                newTileGameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = map.GetTile(i,y).FoodProduced.ToString();
            }
        }
    }

    //Ativar Detalhes Tile
    public void OpenTile(Tile tile)
    {
        detailTile.SetActive(true);

        detailTile.transform.GetChild(0).GetComponent<TextMeshPro>().text = tile.GoldProduced.ToString();
        detailTile.transform.GetChild(1).GetComponent<TextMeshPro>().text = tile.FoodProduced.ToString();
        detailTile.transform.GetChild(2).GetComponent<TextMeshPro>().text = tile.Terrain.ToString();
        
        //filho 4 é com os recurosos todos

    }
}
