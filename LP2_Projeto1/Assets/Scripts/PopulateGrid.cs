using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopulateGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private GameObject _resourcePrefab;

    [SerializeField]
    private GameObject _tileInformation;

    [SerializeField]
    private int numberToCreate;

    [SerializeField]
    private MapContainer _mapContainer;

    [SerializeField]
    private GridLayoutGroup _tilesGrid;

    private void Start()
    {
        _tilesGrid.constraintCount = _mapContainer.Map.Cols;
        int size = GetSizeCells(_mapContainer.Map.Rows, _mapContainer.Map.Cols);
        _tilesGrid.cellSize = new Vector2(size, size);

        numberToCreate = _mapContainer.Map.Rows * _mapContainer.Map.Cols;
        Populate();
    }

    private void Populate()
    {
        GameObject newObj;

        for (int row = 0; row < _mapContainer.Map.Rows; row++)
        {
            for (int col = 0; col < _mapContainer.Map.Cols; col++)
            {
                newObj = (GameObject)Instantiate(prefab, transform);
                newObj.GetComponent<Image>().color = _mapContainer.Map.GetTile(row, col).Color;

                Tile newTile = _mapContainer.Map.GetTile(row, col);

                newObj.GetComponent<Tile>().ChangeTile(
                    newTile.Terrain.ToString().ToLower(), newTile.Resources);

                GridLayoutGroup resourceGrid = newObj.GetComponentInChildren<GridLayoutGroup>();
                SetGridResources(resourceGrid, 3, 15);

                DrawResource(resourceGrid, _mapContainer.Map.GetTile(row, col));
            }
        }
    }

    private void DrawResource(GridLayoutGroup grid, Tile currentTile)
    {
        foreach (Resource r in currentTile.Resources)
        {
            GameObject newResource = Instantiate(_resourcePrefab,
               grid.transform);
                
            newResource.GetComponent<Image>().color = r.Color;
        }
    }

    private int GetSizeCells(int row, int col)
    {
        int menor = row < col ? row : col;
        
        if(col < 7 || col < 5)
            return (int)(740.84f / menor);
        else
            return 100;
    }

    private void SetGridResources(GridLayoutGroup grid, int maxCellsPerRow, int cellSize)
    {
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = maxCellsPerRow; // CHANGE THIS INTO RESOURCE COUNT, PROBABLY NEED TO CREATE A PROPERTY ON MAP CONTAINING THE AVAILABLE RESOURCES

        grid.cellSize = new Vector2(cellSize, cellSize);
    }

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

    public void DestroyChildren(Transform t)
    {
        for(int i = t.childCount - 1 ; i >= 0; i--)
        {
            Destroy(t.GetChild(i).gameObject);
        }
    }
}
