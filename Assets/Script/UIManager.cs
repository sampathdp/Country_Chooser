using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenGamePanal()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ColsePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
