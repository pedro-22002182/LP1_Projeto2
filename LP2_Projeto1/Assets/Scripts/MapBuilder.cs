using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using System;

public class MapBuilder : MonoBehaviour
{
    private Map _map;
    private readonly string[] _availableResources = {"plants","animals","metals","fossilfuel","luxury","pollution"};

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
        int count = 1, currentLine = 0, currentCol = 0;
        string[] fileLines = File.ReadAllLines(FileBrowser.Result[0]);
        ICollection<Resource> currentResources = new List<Resource>();

        _map = new Map(GetMapSize(fileLines[0]));

        while (count <= (_map.Rows * _map.Cols))
        {
            // Atualizar linha atual
            string[] fileLine = fileLines[count].Split();

            // Obter o tipo de terreno
            string currentTerrain = fileLine[0];

            // Limpar a lista de recursos a aplicar ao terreno
            currentResources.Clear();

            // Obter recursos, caso haja algum
            if (fileLine.Length > 1)
            {
                GetResources(fileLine, currentResources);
            }

            _map.SetTile(currentLine, currentCol, new Tile(currentTerrain, currentResources));

            // Ultima coluna ? Passar para a proxima linha : Avancar para a proxima coluna
            if (count % _map.Cols == 0)
            {
                currentLine++;
                currentCol = 0;
                count++;
                continue; // continue passa para a proxima iteracao do ciclo, saltando o codigo que poderia vir a seguir
            }
            else
            {
                currentCol++;
                count++;
            }
        }
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
            if(Array.Exists(_availableResources, r => r == line[i]))
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
