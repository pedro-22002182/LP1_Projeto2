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
        int collumns = map.Cols;
        int rows = map.Rows;

        SetGridLayout(rows, collumns);
        BuildViewMap(rows, collumns);
        

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

                //PASSAR A INFORMAÇÃO AO NOVO GAMEOBCT DO TILE DO MAPA
                Tile newTile = newTileGameObject.GetComponent<Tile>();
                newTile = new Tile(map.GetTile(i,y).Terrain,  map.GetTile(i,y).resources);
                ///////////////////////////////////////////////////////////////
                
                newTileGameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = newTile.GoldProduced.ToString();
                newTileGameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = newTile.FoodProduced.ToString();
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
