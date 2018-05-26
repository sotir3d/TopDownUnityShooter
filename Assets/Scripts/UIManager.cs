using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas victoryCanvas;
    public Canvas uiCanvas;
    public Text ammoCountText;

    public GameObject player;

    public GameManager gameManager;

    void Start()
    {
        //GetComponentInChildren<Canvas>().enabled = false;
        victoryCanvas.enabled = false;

        uiCanvas.enabled = true;
    }

    private void Update()
    {
        ammoCountText.text = "Ammo: " + player.GetComponent<PlayerShoot>().ammoCount;
    }

    public void ToggleVictoryScreen()
    {
        // not the optimal way but for the sake of readability
        if (victoryCanvas.enabled == false)
        {
            victoryCanvas.enabled = true;
            Time.timeScale = 0f;
        }
    }

    public void UpdateAmmoCountText(int currentAmmo)
    {
        ammoCountText.text = "Ammo: " + currentAmmo;
    }
}
