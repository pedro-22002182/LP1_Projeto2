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

    /// <summary>
    /// GameObjects that represent an example to instantiate and build the
    /// visual.
    /// </summary>
    [SerializeField] private GameObject _tile, _detailTile, _resource;

    /// <summary>Responsible for showing tiles in an organization grid.</summary>
    [SerializeField] private GridLayoutGroup _gridTiles;

    private OldMap _map;

    // Start is called before the first frame update
    void Start()
    {
        _map = GameObject.Find("Map").GetComponent<OldMap>();

        int collumns = _map.Cols;
        int rows = _map.Rows;

        SetGridTiles(rows, collumns);
        DrawMap(rows, collumns);

        Destroy(_tile.gameObject);
    }

    private void DrawMap(int lines, int collumns)
    {
        for(int i = 0; i < lines; i++)
        {
            for(int y = 0; y < collumns; y++)
            {
                GameObject newTileGameObject = Instantiate(_tile, transform);

                Tile newTile = _map.GetTile(i,y);

                newTileGameObject.GetComponent<Tile>().ChangeTile(
                    newTile.Terrain.ToString().ToLower(), newTile.Resources);

                newTileGameObject.GetComponent<Image>().color = newTile.Color;
                
                DrawResources(newTileGameObject, newTile);
            }
        }
    }

    private void DrawResources(GameObject newTileGameObject, Tile newTile)
    {
        GridLayoutGroup gridResources = newTileGameObject.transform.
            GetChild(0).GetComponent<GridLayoutGroup>();

        SetGridResources(gridResources, newTile.Resources.Count,
            _gridTiles.cellSize.x, _gridTiles.cellSize.y);

        for(int r = 0; r < newTile.Resources.Count; r++)
        {
            GameObject newResource = Instantiate(_resource,
                newTileGameObject.transform.GetChild(0));
                
            newResource.GetComponent<Image>().color = newTile.Resources[r].Color;
        }
    }

    //Ativar Detalhes Tile
    public void OpenTile(Tile tile)
    {
        DestroyChildren(_detailTile.transform.GetChild(4));
        _detailTile.SetActive(true);

        _detailTile.GetComponent<Image>().color = tile.Color;

        _detailTile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            tile.GoldProduced.ToString();

        _detailTile.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
            tile.FoodProduced.ToString();

        _detailTile.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
            tile.Terrain.ToString();
        
        string resources = "";

        foreach(Resource r in tile.Resources)
        {
            resources += " " + r.Type;
        }

        _detailTile.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
            resources;
        
        GridLayoutGroup gridResources =
            _detailTile.transform.GetChild(4).GetComponent<GridLayoutGroup>();

        SetGridResources(gridResources, tile.Resources.Count, 400, 400);

        for(int r = 0; r < tile.Resources.Count; r++)
        {
            GameObject newResource =
                Instantiate(_resource, _detailTile.transform.GetChild(4));

            newResource.GetComponent<Image>().color = tile.Resources[r].Color;
        }
    }

    private void SetGridTiles(int lines, int collumns)
    {
        _gridTiles.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridTiles.constraintCount = collumns;

        Vector2 sizeScreen =
            transform.parent.GetComponent<RectTransform>().sizeDelta;

        _gridTiles.cellSize = new Vector2(
            sizeScreen.x / collumns, sizeScreen.y / lines);
    }

    private void SetGridResources(GridLayoutGroup grid ,int resourceAmount,
                                    float sizeX, float sizeY)
    {
        if(resourceAmount == 0)
            return;

        grid.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(
                sizeX/1.5f, sizeY/1.5f);
 
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 6; // CHANGE THIS INTO RESOURCE COUNT, PROBABLY NEED TO CREATE A PROPERTY ON MAP CONTAINING THE AVAILABLE RESOURCES

        grid.cellSize = new Vector2((sizeX/1.5f) / 6, (sizeX/1.5f) / 6);
    }

    public void DestroyChildren(Transform t)
    {
        for(int i = t.childCount - 1 ; i >= 0; i--)
        {
            Destroy(t.GetChild(i).gameObject);
        }
    }
}
