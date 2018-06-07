using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{

    public WeaponType currentWeapon;

    public GameObject gameManager;
    public GameObject uiManager;
    public GameObject feet;

    Animator anim;
    Animator animFeet;

    private void Start()
    {
        anim = GetComponent<Animator>();
        animFeet = feet.GetComponent<Animator>();

        currentWeapon = WeaponType.Pistol;
    }

    private void Update()
    {
        if (gameManager.GetComponent<GameManager>().enemyCount == 0)
        {
            uiManager.GetComponent<UIManager>().ToggleVictoryScreen();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public void SetCurrentWeapon(WeaponType newWeapon, int ammo)
    {
        anim.SetInteger("WeaponSwap", (int)newWeapon);
        animFeet.SetInteger("WeaponSwap", (int)newWeapon);
        
        if (newWeapon == WeaponType.Knife)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateMelee;
            GetComponent<PlayerShoot>().ammoCount = 0;
            GetComponent<PlayerShoot>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Pistol)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRatePistol;
            //GetComponent<PlayerShoot>().ammoCount = GlobalValues.ammoCountPistol;
            GetComponent<PlayerShoot>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Rifle)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateRifle;
            //GetComponent<PlayerShoot>().ammoCount = GlobalValues.ammoCountRifle;
            GetComponent<PlayerShoot>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Shotgun)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateShotgun;
            //GetComponent<PlayerShoot>().ammoCount = GlobalValues.ammoCountShotgun;
            GetComponent<PlayerShoot>().currentWeapon = newWeapon;
        }


        GetComponent<PlayerShoot>().ammoCount = ammo;
    }

}
