using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    Animator anim;

    public int currentWeapon;

    public GameObject gameManager;
    public GameObject uiManager;


    private void Start()
    {
        anim = GetComponent<Animator>();

        currentWeapon = 0;
    }

    private void Update()
    {
        if (gameManager.GetComponent<GameManager>().enemyCount == 0)
        {
            gameManager.GetComponent<UIManager>().ToggleVictoryScreen();
        }

        anim.SetInteger("WeaponSwap", currentWeapon);
    }

    public void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
    
    public void SetCurrentWeapon(int newWeapon)
    {
        currentWeapon = newWeapon;

        if (newWeapon == 0)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateMelee;
        }
        else if(newWeapon == 1)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRatePistol;
            GetComponent<PlayerShoot>().ammoCount = GlobalValues.ammoCountPistol;
        }
        else if (newWeapon == 2)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateRifle;
            GetComponent<PlayerShoot>().ammoCount = GlobalValues.ammoCountRifle;
        }
    }

}
