using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas victoryCanvas;
    public Canvas uiCanvas;
    public Canvas pauseCanvas;

    public Text ammoCountText;

    public GameObject player;

    public GameManager gameManager;

    void Start()
    {
        //GetComponentInChildren<Canvas>().enabled = false;
        victoryCanvas.enabled = false;
        pauseCanvas.enabled = false;
        uiCanvas.enabled = true;
    }

    private void Update()
    {
        if(player != null)
        {
            ammoCountText.text = "Ammo: " + player.GetComponent<PlayerShoot>().ammoCount;
        }
    }

    public void ToggleVictoryScreen()
    {
        // not the optimal way but for the sake of readability
        if (victoryCanvas.enabled == false)
        {
            victoryCanvas.enabled = true;
            uiCanvas.enabled = false;
        }
    }

    public void TogglePauseScreen()
    {
        if (pauseCanvas.enabled == false)
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
}
