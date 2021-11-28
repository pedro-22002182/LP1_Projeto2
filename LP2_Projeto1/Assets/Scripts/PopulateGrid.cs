using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopulateGrid : MonoBehaviour
{   
    
    // ///////////////////////
    // Instance variables  //
    // /////////////////////
    

    /// <summary> Tile prefab to be instanciated. </summary>
    [SerializeField]
    private GameObject tilePrefab;

    /// <summary> Resource prefab to be instanciated. </summary>
    [SerializeField]
    private GameObject _resourcePrefab;

    /// <summary> Tile with detailed information. </summary>
    [SerializeField]
    private GameObject _tileInformation;
    
    [SerializeField]
    private int numberToCreate;

    /// <summary> Map information is contained here. </summary>
    [SerializeField]
    private MapContainer _mapContainer;

    /// <summary> Group of tiles. </summary>
    [SerializeField]
    private GridLayoutGroup _tilesGrid;

    /// <summary>
    /// Define the size of map variables.
    /// </summary>
    private void Start()
    {
        _tilesGrid.constraintCount = _mapContainer.Map.Cols;
        int size = GetSizeCells(_mapContainer.Map.Rows, _mapContainer.Map.Cols);
        _tilesGrid.cellSize = new Vector2(size, size);

        numberToCreate = _mapContainer.Map.Rows * _mapContainer.Map.Cols;
        Populate();
    }

    /// <summary>
    /// Visual representation of the terrain type and the resources that each of them contains.
    /// </summary>
    private void Populate()
    {
        GameObject newObj;

        //Cycle through each row and colum.
        for (int row = 0; row < _mapContainer.Map.Rows; row++)
        {
            for (int col = 0; col < _mapContainer.Map.Cols; col++)
            {   
                //Instantiate tile with its respective information.
                newObj = (GameObject)Instantiate(tilePrefab, transform);
                newObj.GetComponent<Image>().color = _mapContainer.Map.GetTile(row, col).Color;

                Tile newTile = _mapContainer.Map.GetTile(row, col);

                newObj.GetComponent<Tile>().ChangeTile(
                    newTile.Terrain.ToString().ToLower(), newTile.Resources);
                
                //Instantiate resources of each tile.
                GridLayoutGroup resourceGrid = newObj.GetComponentInChildren<GridLayoutGroup>();
                SetGridResources(resourceGrid, 3, 15);

                DrawResource(resourceGrid, _mapContainer.Map.GetTile(row, col));
            }
        }
    }


    /// <summary>
    /// Define the sizes of the table of tiles, ir order to fill the entire screen(Main Grid).
    /// </summary>
    /// <param name="row">Represents lines of the map</param>
    /// <param name="col">Represents colums of the map</param>
    /// <returns></returns>
    private int GetSizeCells(int row, int col)
    {
        int menor = row < col ? row : col;
        
        if(col < 7 || col < 5)
            return (int)(740.84f / menor);
        else
            return 100;
    }

    /// <summary>
    /// Instantiate resources in each tile with corresponding visual representation.
    /// </summary>
    /// <param name="grid">Represents the table where the resources of a specific tile are located</param>
    /// <param name="currentTile">Specific tile</param>
    private void DrawResource(GridLayoutGroup grid, Tile currentTile)
    {
        foreach (Resource r in currentTile.Resources)
        {
            GameObject newResource = Instantiate(_resourcePrefab,
               grid.transform);
                
            newResource.GetComponent<Image>().color = r.Color;
        }
    }


    /// <summary>
    /// Construction of the table of resources of each tile.
    /// </summary>
    /// <param name="grid">Represents the table where the resources of a specific tile are located</param>
    /// <param name="maxCellsPerRow">Max number of cells in each row</param>
    /// <param name="cellSize">Size of resource cell</param>
    private void SetGridResources(GridLayoutGroup grid, int maxCellsPerRow, int cellSize)
    {
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = maxCellsPerRow; // CHANGE THIS INTO RESOURCE COUNT, PROBABLY NEED TO CREATE A PROPERTY ON MAP CONTAINING THE AVAILABLE RESOURCES

        grid.cellSize = new Vector2(cellSize, cellSize);
    }

    /// <summary>
    /// Contains the detailed information of any specific tile once pressed.
    /// </summary>
    /// <param name="tile"> Represents specific tile</param>
    public void OpenTile(Tile tile)
    {
        DestroyChildren(_tileInformation.transform.GetChild(4));
        _tileInformation.SetActive(true);

        _tileInformation.GetComponent<Image>().color = tile.Color;

        _tileInformation.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Gold produced: " + tile.GoldProduced.ToString();

        _tileInformation.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
            "Food produced: " + tile.FoodProduced.ToString();

        _tileInformation.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
            "Terrain: " + tile.Terrain.ToString();
        
        string resources = "";

        foreach(Resource r in tile.Resources)
        {
            resources += " " + r.Type;
        }

        _tileInformation.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
            "Resources: " + resources;
        
        GridLayoutGroup gridResources =
            _tileInformation.transform.GetChild(4).GetComponent<GridLayoutGroup>();

        SetGridResources(gridResources, 6, 30);

        for(int r = 0; r < tile.Resources.Count; r++)
        {
            GameObject newResource =
                Instantiate(_resourcePrefab, _tileInformation.transform.GetChild(4));

            newResource.GetComponent<Image>().color = tile.Resources[r].Color;
        }
    }

    /// <summary>
    /// Destroy all children of gameobject
    /// </summary>
    /// <param name="t"></param>
    public void DestroyChildren(Transform t)
    {
        for(int i = t.childCount - 1 ; i >= 0; i--)
        {
            Destroy(t.GetChild(i).gameObject);
        }
    }
}
