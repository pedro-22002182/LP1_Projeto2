using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// The <c>PopulateGrid</c> class.
/// Responsible for drawing the map, based on its information.
/// </summary>
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

    /// <summary> Grid where the tiles will be placed of the screen. </summary>
    [SerializeField]
    private GridLayoutGroup _tilesGrid;

    [SerializeField]
    private TextMeshProUGUI textButFuture;

    /// <summary>
    /// Sets up the grid and populates it with the visual representation of
    /// the tiles.
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
    /// Populates a grid with the visual representation of each tile contained
    /// on the map.
    /// </summary>
    private void Populate()
    {
        GameObject newObj;

        //Cycle through each row and colum.
        for (int row = 0; row < _mapContainer.Map.Rows; row++)
        {
            for (int col = 0; col < _mapContainer.Map.Cols; col++)
            {   
                //Instantiate tile with its respective information
                newObj = (GameObject)Instantiate(tilePrefab, transform);

                // Change the image's color to the tile's color
                newObj.GetComponent<Image>().color =
                    _mapContainer.Map.GetTile(row, col).Color;

                // Get reference to the current tile so we can get its information
                Tile newTile = _mapContainer.Map.GetTile(row, col);
                newObj.GetComponent<Tile>().ChangeTile(
                    newTile.Terrain.ToString().ToLower(), newTile.Resources);
                
                //Instantiate resources of each tile
                GridLayoutGroup resourceGrid =
                    newObj.GetComponentInChildren<GridLayoutGroup>();
                
                // Setup the grid where the tile's resources will be shown
                SetGridResources(resourceGrid, 3, 15);

                // Draw the visual representation of the tile's resources
                DrawResource(resourceGrid, _mapContainer.Map.GetTile(row, col));
            }
        }
    }

    /// <summary>
    /// Define the sizes of the table of tiles, ir order to fill the entire
    /// screen(Main Grid).
    /// </summary>
    /// <param name="row">Represents lines of the map.</param>
    /// <param name="col">Represents columns of the map.</param>
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
    /// <param name="grid">
    /// Grid where the resources of a specific tile will be shown.
    /// </param>
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
    /// Sets up the grid where the resources of each tile will be shown.
    /// </summary>
    /// <param name="grid">
    /// Grid where the resources of a specific tile will be shown.
    /// </param>
    /// <param name="maxCellsPerRow">Max number of cells in each row.</param>
    /// <param name="cellSize">Size of resource cell.</param>
    private void SetGridResources(GridLayoutGroup grid, int maxCellsPerRow, 
                                    int cellSize)
    {
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = maxCellsPerRow;
        grid.cellSize = new Vector2(cellSize, cellSize);
    }

    /// <summary>
    /// Contains the detailed information of any specific tile once pressed.
    /// </summary>
    /// <param name="tile"> Represents specific tile</param>
    public void OpenTile(Tile tile)
    {
        // Clear information from the last pressed tile
        DestroyChildren(_tileInformation.transform.GetChild(4));

        // Activate tile informartion
        _tileInformation.SetActive(true);

        // Change color to the pressed tile's color
        _tileInformation.GetComponent<Image>().color = tile.Color;

        // Set the amount of gold produced
        _tileInformation.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =
            "Gold produced: " + tile.GoldProduced.ToString();

        // Set the amount of food produced
        _tileInformation.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
            "Food produced: " + tile.FoodProduced.ToString();

        // Set the type of terain
        _tileInformation.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
            "Terrain: " + tile.Terrain.ToString();
        
        // Set the resources
        string resources = "";

        foreach(Resource r in tile.Resources)
        {
            resources += " " + r.Type;
        }

        _tileInformation.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text =
            "Resources: " + resources;
        
        // Get reference of the grid where the resources will be displayed
        GridLayoutGroup gridResources =
            _tileInformation.transform.GetChild(4).GetComponent<GridLayoutGroup>();

        // Setup the grid
        SetGridResources(gridResources, 6, 30);

        // Display the resources
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

    public void TilesTwoOrMoreResor()
    {
        textButFuture.text = "Tiles With Two or More Resources = " + _mapContainer.Map.TilesWithTwoOrMoreRecurses();
    }

    public void FoodDesert()
    {
        textButFuture.text = "Total Food in Desert = " + _mapContainer.Map.TotalFoodInDesert();
    }

    public void TilesWith3MoreRes()
    {
        string s = "";
        IEnumerable<Tile> enumerable = _mapContainer.Map.TilesWith3More();

        foreach(Tile t in enumerable)
        {
            s += t.Terrain + " |";

            foreach(Resource r in t.Resources)
            {
                s += r.Type + ", ";
            }

            s += "| coord = " + _mapContainer.Map.GetPosTile(t)[0] + ", " + _mapContainer.Map.GetPosTile(t)[1];

            s += "\n";
        }

        textButFuture.text = s;
    }

    public void AnyGrassLandWithLuxury()
    {
        textButFuture.text = "Existe GrassLand com Luxury? \n Resposta = " + _mapContainer.Map.AnyGrassWithLux();
    }

    public void TileMoreFood()
    {
        string s = "";
        Tile tileWithMoreFood = _mapContainer.Map.MoreFood();

        s += tileWithMoreFood.Terrain + " |";

        foreach(Resource r in tileWithMoreFood.Resources)
        {
            s += r.Type + ", ";
        }

        s += "| coord = " + _mapContainer.Map.GetPosTile(tileWithMoreFood)[0] + ", " + _mapContainer.Map.GetPosTile(tileWithMoreFood)[1];

        s += " | Food = " + tileWithMoreFood.FoodProduced;

        textButFuture.text = s;
    }
}
