using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The <c>View</c> class.
/// Class responsable for showing the data (map, tiles, resources) to the user.
/// </summary>
public class View : MonoBehaviour
{
    // ///////////////////////
    // Instance variables  //
    // /////////////////////

    /// <summary>GameObjects that represent a exemple for instantiate and build the visual.</summary>
    [SerializeField] private GameObject tile, detailTile, resource;

    /// <summary>Responsible for showing tiles in a organization grid.</summary>
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

    private void BuildViewMap(int lines, int collumns)
    {
        for(int i = 0; i < lines; i++)
        {
            for(int y = 0; y < collumns; y++)
            {
                GameObject newTileGameObject = Instantiate(tile, transform);
                
                Debug.Log(newTileGameObject.name);

                Tile newTile = map.GetTile(i,y);

                newTileGameObject.GetComponent<Tile>().ChangeTile(newTile.Terrain.ToString().ToLower(), newTile.Resources);
                newTileGameObject.GetComponent<Image>().color = newTile.Color;
                
                GridLayoutGroup gridResources = newTileGameObject.transform.GetChild(0).GetComponent<GridLayoutGroup>();
                SetGridResources(gridResources, newTile.Resources.Count, gridTiles.cellSize.x, gridTiles.cellSize.y);

                for(int r = 0; r < newTile.Resources.Count; r++)
                {
                    GameObject newResource = Instantiate(resource, newTileGameObject.transform.GetChild(0));
                    newResource.GetComponent<Image>().color = newTile.Resources[r].Color;
                }
                
            }
        }
    }


    private void SetGridTiles(int lines, int collumns)
    {
        gridTiles.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridTiles.constraintCount = collumns;

        gridTiles.cellSize = new Vector2(1600 / collumns, 900 / lines);
    }


    //MUDAR PARA SER AUTOMATICO
    private void SetGridResources(GridLayoutGroup grid ,int num, float sizeX, float sizeY)
    {
        if(num == 0)
            return;

        grid.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX/1.5f, sizeY/1.5f);
 
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = Mathf.RoundToInt(num/2) + 1;

        grid.cellSize = new Vector2(sizeY / 5, sizeY / 5);
    }

    //Ativar Detalhes Tile
    public void OpenTile(Tile tile)
    {
        DestroyChildren(detailTile.transform.GetChild(4));
        detailTile.SetActive(true);

        detailTile.GetComponent<Image>().color = tile.Color;
        detailTile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tile.GoldProduced.ToString();
        detailTile.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tile.FoodProduced.ToString();
        detailTile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = tile.Terrain.ToString();
        
        string resources = "";

        foreach(Resource r in tile.Resources)
        {
            resources += " " + r.Type;
        }

        detailTile.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = resources;
        
        GridLayoutGroup gridResources = detailTile.transform.GetChild(4).GetComponent<GridLayoutGroup>();
        SetGridResources(gridResources, tile.Resources.Count, 400, 400);

        for(int r = 0; r < tile.Resources.Count; r++)
        {
            GameObject newResource = Instantiate(resource, detailTile.transform.GetChild(4));
            newResource.GetComponent<Image>().color = tile.Resources[r].Color;
        }
    }

    public void DestroyChildren(Transform t)
    {
        for(int i = t.GetChildCount() - 1 ; i >= 0; i--)
        {
            Destroy(t.GetChild(i).gameObject);
        }
    }
}
