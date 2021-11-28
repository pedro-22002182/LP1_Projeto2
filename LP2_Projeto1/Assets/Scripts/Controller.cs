using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;

/// <summary>
/// <c>The Controller class<c>
/// Its responsable for searching files on the users computer.
/// It can only open files with .map4x extension.
/// Based on the information in this file, a map is created.
/// </summary>
public class Controller : MonoBehaviour
{
    [SerializeField]
    private MapContainer _mapContainer;

    /// <summary>
    /// This allows the player to choose a file while excluding any file type that is not .map4x.
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
    /// <returns></returns>
    private IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select a file", "Select");
        Debug.Log( FileBrowser.Success );

        if( FileBrowser.Success )
		{
            BuildMap();
            SceneManager.LoadScene("Game");
		}
    }

    /// <summary>
    /// Build map based on the file information provided
    /// </summary>
    private void BuildMap()
    {
        /// <summary>
        /// Read File
        /// </summary>
        int count = 1, currentLine = 0, currentCol = 0;
        string[] fileLines = File.ReadAllLines(FileBrowser.Result[0]);
        ICollection<Resource> currentResources = new List<Resource>();

        //_map = new Map(GetMapSize(fileLines[0]));
        _mapContainer.Map.SetSize(GetMapSize(fileLines[0]));

        while (count <= (_mapContainer.Map.Rows * _mapContainer.Map.Cols))
        {
            // Update current line of text
            string[] fileLine = fileLines[count].Split();

            // The first word in each line represents the terrain type
            string currentTerrain = fileLine[0];

            // Clear current resource list and apply it to the terrain 
            currentResources.Clear();

            // Check if there are any resources and if so check what they are 
            if (fileLine.Length > 1)
            {
                GetResources(fileLine, currentResources);
            }

            _mapContainer.Map.SetTile(currentLine, currentCol, new Tile(currentTerrain, currentResources));

            // If its the last collum, skip to the next line 
            if (count % _mapContainer.Map.Cols == 0)
            {
                currentLine++;
                currentCol = 0;
                count++;
                continue; 
            }
            else
            {
                currentCol++;
                count++;
            }
        }
    }

    /// <summary>
    /// This returns the size of the map based on the first line of text
    /// </summary>
    /// <param name="rows">Number of rows in map</param>
    /// <param name="line">Number of Colums in map</param>
    /// <returns></returns>
    private (int rows, int cols) GetMapSize(string line)
    {
        int rows, cols;
        rows = Int32.Parse(line.Split()[0]);
        cols = Int32.Parse(line.Split()[1]);

        return (rows, cols);
    }
    /// <summary>
    /// This cycles through all lines of text and gets the resource types present in each terrain
    /// # this symbol represents when a new row begins
    /// </summary>
    /// <param name="line">If the line has more than 1 word, this means we create an array 
    /// and each word afther the first one represents a resource.
    /// </param>
    /// <param name="resourceList">This represents a list of the current resources on each terrain</param>
    private void GetResources(string[] line, ICollection<Resource> resourceList)
    {
        for(int i = 1; i < line.Length; i++)
        {
            if(Array.Exists(TestMap.AvailableResources, r => r == line[i]))
            {
                resourceList.Add(new Resource(line[i]));
            }
            else if(line[i] == "#") break;
        }
    }

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
    /// Loads a new scene
    /// </summary>
    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
