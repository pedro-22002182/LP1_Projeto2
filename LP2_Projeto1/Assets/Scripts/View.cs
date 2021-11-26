using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class View : MonoBehaviour
{
    [SerializeField] private GameObject tile, detailTile, resource;
    [SerializeField] private GridLayoutGroup gridTiles;

    private Map map;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();

        int collumns = map.Cols;
        int rows = map.Rows;

        SetGridTiles(rows, collumns);
        BuildViewMap(rows, collumns);

        Destroy(tile.gameObject);
    }

    private void SetGridTiles(int lines, int collumns)
    {
        gridTiles.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridTiles.constraintCount = collumns;

        gridTiles.cellSize = new Vector2(1600 / collumns, 900 / lines);
    }

    private void BuildViewMap(int lines, int collumns)
    {
        for(int i = 0; i < lines; i++)
        {
            for(int y = 0; y < collumns; y++)
            {
                GameObject newTileGameObject = Instantiate(tile, transform);
                
                Debug.Log(newTileGameObject.name);

                Tile newTile = map.GetTile(i,y);

                newTileGameObject.GetComponent<Image>().color = newTile.color;
                
                GridLayoutGroup gridResources = newTileGameObject.transform.GetChild(0).GetComponent<GridLayoutGroup>();
                SetGridResources(gridResources, newTile.Resources.Count);

                for(int r = 0; r < newTile.Resources.Count; r++)
                {
                    GameObject newResource = Instantiate(resource, newTileGameObject.transform.GetChild(0));
                    newResource.GetComponent<Image>().color = newTile.Resources[r].color;
                }
                
                Destroy(gridResources.gameObject.transform.GetChild(0).gameObject);
            }
        }
    }

    //MUDAR PARA SER AUTOMATICO
    private void SetGridResources(GridLayoutGroup grid ,int num)
    {
        if(num == 0)
            return;
 
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 6;

        grid.cellSize = new Vector2(160 / 6, 160 / 6);
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
