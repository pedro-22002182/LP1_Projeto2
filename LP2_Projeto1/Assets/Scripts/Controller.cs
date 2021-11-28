using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;

/// <summary>
/// <c>The Controller class<c>
/// It is responsible for searching files on the users computer.
/// It can only open files with .map4x extension.
/// Based on the information in this file, a map is created.
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField]
    private MapContainer _mapContainer;

    [SerializeField]
    private GameObject _canvasError;

    /// <summary>
    /// Sets up the File Browser and starts the coroutine that asks the user for
    /// a file.
    /// The loaded file will contain the information for the map creation.
    /// </summary>
    public void ChooseFile()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Maps", ".map4x"));
        FileBrowser.SetDefaultFilter(".map4x");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    /// <summary>
    /// This represents the visual part of the file selection process.
    /// Once a file is selected, if its a correct file the map will be built 
    /// based on that information on the "Game" scene.
    /// </summary>
    /// <returns>The path of the chosen file.</returns>
    private IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files,
            false, null, null, "Select a file", "Select");

        if( FileBrowser.Success )
		{
            BuildMap();
            SceneManager.LoadScene("Game");
		}
    }

    /// <summary>
    /// Builds the map based on the information provided on the file.
    /// </summary>
    private void BuildMap()
    {
        // Auxiliary variables
        int count = 1, currentLine = 0, currentCol = 0;
        ICollection<Resource> currentResources = new List<Resource>();

        // Array containing all the lines of the file
        string[] fileLines = File.ReadAllLines(FileBrowser.Result[0]);

        // Set the map size and see if the numbers are over 0
        if(_mapContainer.Map.SetSize(GetMapSize(fileLines[0])) == false)
        {
            FoundError();
            return;
        }
        
        // Go through all the lines on the file
        while (count <= (_mapContainer.Map.Rows * _mapContainer.Map.Cols))
        {
            // Update current line of text
            string[] fileLine = fileLines[count].Split();

            // The first word in each line represents the terrain type
            string currentTerrain = fileLine[0];

            // Clear the resource list in each cycle so we don't get resources
            // from the last iteration
            currentResources.Clear();

            // Check if there are any resources and if so check what they are 
            if (fileLine.Length > 1)
            {
                GetResources(fileLine, currentResources);
            }

            // Place a new tile on the map
            _mapContainer.Map.SetTile(currentLine, currentCol, new Tile(
                currentTerrain, currentResources));

            // If its the last column, skip to the next line 
            if (count % _mapContainer.Map.Cols == 0)
            {
                currentLine++;
                currentCol = 0;
                count++;
                continue; 
            }
            else // otherwise go to the next column
            {
                currentCol++;
                count++;
            }
        }
    }

    /// <summary>
    /// This returns the size of the map based on the first line of text.
    /// </summary>
    /// <param name="rows">Number of rows in the map.</param>
    /// <param name="line">Number of columns in map.</param>
    /// <returns>Map size.</returns>
    private (int rows, int cols) GetMapSize(string line)
    {
        int rows, cols;
        rows = Int32.Parse(line.Split()[0]);
        cols = Int32.Parse(line.Split()[1]);

        return (rows, cols);
    }
    /// <summary>
    /// This cycles through all lines of text and gets the resource types present
    /// in each terrain.
    /// # -> This symbol represents a comment, which is ignored.
    /// </summary>
    /// <param name="line">
    /// The line of text that is currently being analyzed.
    /// </param>
    /// <param name="resourceList">
    /// List to which the resources will be added, and then will be applied
    /// to the tile.
    /// </param>
    private void GetResources(string[] line, ICollection<Resource> resourceList)
    {
        for(int i = 1; i < line.Length; i++)
        {
            if(Array.Exists(Map.AvailableResources, r => r == line[i]))
            {
                resourceList.Add(new Resource(line[i]));
            }
            else if(line[i] == "#") break;
        }
    }


    public void FoundError()
    {
        _canvasError.SetActive(true);
    }

    /// <summary>
    /// Closes or stops the application, depending on if a build is running or
    /// the application is being played on the Unity editor.
    /// </summary>
    public void QuitApplication()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    /// <summary>
    /// Loads the Instructions scene.
    /// </summary>
    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene("Instructions");
    }

    /// <summary>
    /// Loads the MainMenu scene.
    /// </summary>
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
