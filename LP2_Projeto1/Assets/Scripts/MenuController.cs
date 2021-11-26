using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonsContainer;
    private bool IsMenu;
    public void CloseGame()
    {
        Application.Quit();
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
