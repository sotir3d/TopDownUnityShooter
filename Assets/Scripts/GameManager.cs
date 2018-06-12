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

    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(!gamePaused)
                PauseGame(true);
            else
                PauseGame(false);
        }
    }

    void PauseGame(bool pauseState)
    {
        gamePaused = pauseState;

        if (gamePaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

        uiManager.GetComponent<UIManager>().TogglePauseScreen();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
    
    public void OpenScene(int sceneNumber)
    {
        Debug.Log("pressed");
        SceneManager.LoadScene(sceneNumber);
    }

    public void ToggleVictoryScreen()
    {
        uiManager.GetComponent<UIManager>().ToggleVictoryScreen();
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
