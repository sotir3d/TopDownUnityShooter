using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public int enemyCount;

    public UIManager uiManager;

    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    bool gamePaused = false;
    bool levelComplete = false;

    private void Start()
    {
        TimeFrozen(false);
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
        if (!levelComplete && uiManager != null)
        {
            if (gamePaused)
            {
                TimeFrozen(true);
                uiManager.GetComponent<UIManager>().SetPauseScreen(true);
            }
            else
            {
                TimeFrozen(false);
                uiManager.GetComponent<UIManager>().SetPauseScreen(false);
            }

        }
    }

    public void TimeFrozen(bool isFrozen)
    {
        if (paused == null || unpaused == null)
            return;

        if(isFrozen)
        {
            Time.timeScale = 0;
            paused.TransitionTo(0.01f);
        }
        else
        {
            Time.timeScale = 1;
            unpaused.TransitionTo(0.01f);
        }
    }

    public void ContinueGame()
    {
        TimeFrozen(false);
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
        TimeFrozen(true);
        uiManager.GetComponent<UIManager>().ToggleVictoryScreen();
        levelComplete = true;
    }

    public void ToggleFailedScreen()
    {
        TimeFrozen(true);
        uiManager.GetComponent<UIManager>().ToggleFailedScreen();
    }

    public void RestartScene()
    {
        TimeFrozen(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < (SceneManager.sceneCountInBuildSettings - 1))
        {
            TimeFrozen(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
