using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonsContainer;

    private GameObject MapContainer;

    private bool IsMenu;

    private void Start()
    {
        MapContainer = GameObject.Find("Map");
    }

    public void CloseGame()
    {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OpenAnotherMapFile()
    {
        Destroy(MapContainer);
        SceneManager.LoadScene("StartMenu");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("+ressed");
            IsMenu = !IsMenu;
            if(IsMenu)
            {
                ButtonsContainer.SetActive(true);
            }
            else
            {
                ButtonsContainer.SetActive(false);
            }
        }
    }
    
}
