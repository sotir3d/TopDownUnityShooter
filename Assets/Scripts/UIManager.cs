using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Canvas victoryCanvas;
    public Canvas uiCanvas;
    public Canvas pauseCanvas;
    public Canvas failedCanvas;

    public Text ammoCountText;
    public Text timerText;

    public Text highScore;
    public Text currentScore;

    public GameObject nextLevelButton;

    public GameObject player;

    public GameManager gameManager;

    TimeSpan currentTimeSpan;

    void Start()
    {
        //GetComponentInChildren<Canvas>().enabled = false;
        victoryCanvas.enabled = false;
        pauseCanvas.enabled = false;
        failedCanvas.enabled = false;
        uiCanvas.enabled = true;
    }

    private void Update()
    {
        if (player != null)
        {
            ammoCountText.text = "Ammo: " + player.GetComponent<PlayerWeapon>().ammoCount;

            timerText.text = ConvertFloatTimeToStringTime(Time.timeSinceLevelLoad);
        }
    }

    public void ToggleVictoryScreen()
    {
        currentScore.text = ConvertFloatTimeToStringTime(Time.timeSinceLevelLoad);
        highScore.text = ConvertFloatTimeToStringTime(PlayerPrefs.GetFloat("Level" + SceneManager.GetActiveScene().buildIndex));
        victoryCanvas.enabled = true;
        uiCanvas.enabled = false;
    }

    string ConvertFloatTimeToStringTime(float time)
    {
        TimeSpan convertTimeSpan;

        convertTimeSpan = TimeSpan.FromSeconds(time);

        return convertTimeSpan.Minutes.ToString("00") + ":" + convertTimeSpan.Seconds.ToString("00") + "." + convertTimeSpan.Milliseconds / 100;
    }

    public void ToggleFailedScreen()
    {
        failedCanvas.enabled = true;
        uiCanvas.enabled = false;
    }

    public void SetPauseScreen(bool ispaused)
    {
        if (ispaused == true)
        {
            pauseCanvas.enabled = true;
            uiCanvas.enabled = false;
        }
        else
        {
            pauseCanvas.enabled = false;
            uiCanvas.enabled = true;
        }
    }

    public void UpdateAmmoCountText(int currentAmmo)
    {
        ammoCountText.text = "Ammo: " + currentAmmo;
    }

    public void ToggleNextLevelButton(bool isActive)
    {
        nextLevelButton.SetActive(isActive);
    }
}
