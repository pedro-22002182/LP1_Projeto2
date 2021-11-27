using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonsContainer;
    private bool IsMenu;
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
        SceneManager.LoadScene("StartMenu");
    }
    void update()
    {
        if(Input.GetKeyDown(KeyCode.E))
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
