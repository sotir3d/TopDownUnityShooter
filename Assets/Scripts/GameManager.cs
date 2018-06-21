using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int enemyCount;

    public UIManager uiManager;

    bool gamePaused = false;
    bool levelComplete = false;

    private void Start()
    {
        if(uiManager!=null)
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
                uiManager.GetComponent<UIManager>().ToggleNextLevelButton(true);
            else
                uiManager.GetComponent<UIManager>().ToggleNextLevelButton(false);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!gamePaused)
                PauseGame(true);
            else
                PauseGame(false);
        }
    }

    void PauseGame(bool pauseState)
    {
        gamePaused = pauseState;
        if (!levelComplete)
        {
            if (gamePaused)
            {
                Time.timeScale = 0;
                uiManager.GetComponent<UIManager>().SetPauseScreen(true);
            }
            else
            {
                Time.timeScale = 1;
                uiManager.GetComponent<UIManager>().SetPauseScreen(false);
            }

        }
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        uiManager.GetComponent<UIManager>().SetPauseScreen(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void OpenScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void ToggleVictoryScreen()
    {
        uiManager.GetComponent<UIManager>().ToggleVictoryScreen();
        levelComplete = true;
        Time.timeScale = 0f;
    }

    public void ToggleFailedScreen()
    {
        uiManager.GetComponent<UIManager>().ToggleFailedScreen();

        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < (SceneManager.sceneCountInBuildSettings - 1))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
