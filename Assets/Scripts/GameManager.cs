using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int enemyCount;

    public UIManager UI;

    // Use this for initialization
    void Start()
    {        
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
}
