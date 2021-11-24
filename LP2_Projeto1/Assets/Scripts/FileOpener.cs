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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
