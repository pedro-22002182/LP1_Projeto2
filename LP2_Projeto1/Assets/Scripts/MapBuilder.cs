using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using System;

public class MapBuilder : MonoBehaviour
{
    private Map map;
    private readonly string[] availableResources = {"plants","animals","metals","fossilfuel","luxury","pollution"};

    // Start is called before the first frame update
    void Start()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Maps", ".map4x"));
        FileBrowser.SetDefaultFilter(".map4x");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    private IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select a file", "Select");
        Debug.Log( FileBrowser.Success );

        if( FileBrowser.Success )
		{
			for( int i = 0; i < FileBrowser.Result.Length; i++ )
				Debug.Log( FileBrowser.Result[i] );

			// Read the bytes of the first file via FileBrowserHelpers
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0] );

			// Or, copy the first file to persistentDataPath
			string destinationPath = Path.Combine( Application.persistentDataPath, FileBrowserHelpers.GetFilename( FileBrowser.Result[0] ) );
			FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );
        
		}
    }

    private void BuildMap()
    {
        //ler ficheiro
        string[] fileLines = File.ReadAllLines(FileBrowser.Result[0]);
        ICollection<Resource> currentResources = new List<Resource>();

        map = new Map(GetMapSize(fileLines[0]));


        //construir mapa consoante ficheiro
        //mudar de scene com o mapa e depois quando inicia na outra scene a view capta logo o mapa e gg
    }

    private (int rows, int cols) GetMapSize(string line)
    {
        int rows, cols;
        rows = Int32.Parse(line.Split()[0]);
        cols = Int32.Parse(line.Split()[1]);

        return (rows, cols);
    }

    private void GetResources(string[] line, ICollection<Resource> resourceList)
    {
        for(int i = 1; i < line.Length; i++)
        {
            if(Array.Exists(availableResources, r => r == line[i]))
            {
                resourceList.Add(new Resource(line[i]));
            }
            else if(line[i] == "#") break;
            else
            {
                Debug.Log("Ficheiro errado");
                //FAZER BOTAO PARA ABRIR SCENE NOVAMENTE
            }

        }
    }
}
