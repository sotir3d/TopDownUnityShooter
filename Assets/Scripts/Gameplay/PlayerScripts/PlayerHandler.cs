using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    Animator anim;

    public WeaponType currentWeapon;

    public GameObject gameManager;
    public GameObject uiManager;

    bool swapWeapon = false;


    private void Start()
    {
        anim = GetComponent<Animator>();

        currentWeapon = WeaponType.Pistol;
    }

    private void Update()
    {
        if (gameManager.GetComponent<GameManager>().enemyCount == 0)
        {
            uiManager.GetComponent<UIManager>().ToggleVictoryScreen();
        }

        if (swapWeapon == true)
        {
            anim.SetInteger("WeaponSwap", (int)currentWeapon);
            swapWeapon = false;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public void SetCurrentWeapon(WeaponType newWeapon)
    {
        currentWeapon = newWeapon;
        swapWeapon = true;

        if (newWeapon == WeaponType.Knife)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateMelee;
            GetComponent<PlayerShoot>().ammoCount = 0;
            GetComponent<PlayerShoot>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Pistol)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRatePistol;
            GetComponent<PlayerShoot>().ammoCount = GlobalValues.ammoCountPistol;
            GetComponent<PlayerShoot>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Rifle)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateRifle;
            GetComponent<PlayerShoot>().ammoCount = GlobalValues.ammoCountRifle;
            GetComponent<PlayerShoot>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Shotgun)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateShotgun;
            GetComponent<PlayerShoot>().ammoCount = GlobalValues.ammoCountShotgun;
            GetComponent<PlayerShoot>().currentWeapon = newWeapon;
        }
    }

}
