using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;

public class FileOpener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Maps", ".map4x"));
        FileBrowser.SetDefaultFilter(".map4x");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
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
}
