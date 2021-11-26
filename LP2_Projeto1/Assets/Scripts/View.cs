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
        map = GameObject.Find("Map").GetComponent<Map>();

        int collumns = map.Cols;
        int rows = map.Rows;
        Debug.Log(rows + " " + collumns);

        SetGridLayout(rows, collumns);
        BuildViewMap(rows, collumns);

        Destroy(tile.gameObject);
    }

    private void SetGridLayout(int lines, int collumns)
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = collumns;

        gridLayout.cellSize = new Vector2(1600 / collumns, 900 / lines);
        Debug.Log(lines + " " + collumns);
    }

    private void BuildViewMap(int lines, int collumns)
    {
        for(int i = 0; i < lines; i++)
        {
            for(int y = 0; y < collumns; y++)
            {
                GameObject newTileGameObject = Instantiate(tile, transform);
                
                Debug.Log(newTileGameObject.name);
                newTileGameObject.GetComponent<Image>().color = map.GetTile(i,y).color;
                newTileGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = map.GetTile(i,y).GoldProduced.ToString();
                newTileGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = map.GetTile(i,y).FoodProduced.ToString();

                //PASSAR A INFORMAÇÃO AO NOVO GAMEOBCT DO TILE DO MAPA
               // Tile newTile = newTileGameObject.GetComponent<Tile>();
               // newTile = new Tile(map.GetTile(i,y).Terrain.ToString().ToLower(),  map.GetTile(i,y).Resources);
                ///////////////////////////////////////////////////////////////
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
