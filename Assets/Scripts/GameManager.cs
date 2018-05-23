using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemyCount;

    public UIManager UI;

    // Use this for initialization
    void Start()
    {
        GetComponentInChildren<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    public void ToggleVictoryScreen()
    {
        // not the optimal way but for the sake of readability
        if (GetComponentInChildren<Canvas>().enabled == false)
        {
            GetComponentInChildren<Canvas>().enabled = true;
            Time.timeScale = 0f;
        }
    }
}
