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

    public GameObject bloodSpatter;
    public GameObject bloodDecal;

    public AudioSource audioSource;
    public AudioClip[] bloodSpatterSound;

    Animator anim;
    Animator animFeet;

    bool deathTriggered = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        animFeet = feet.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        currentWeapon = WeaponType.Pistol;
    }

    private void Update()
    {
        if (gameManager.GetComponent<GameManager>().enemyCount == 0)
        {
            Invoke("ToggleVictoryScreen", 0.5f);
        }
    }

    void ToggleVictoryScreen()
    {
        if (!deathTriggered)
            gameManager.GetComponent<GameManager>().ToggleVictoryScreen();
    }

    public void Death()
    {
        if (!deathTriggered)
        {
            deathTriggered = true;

            Instantiate(bloodSpatter, transform.position, transform.rotation);
            Instantiate(bloodDecal, transform.position, transform.rotation);

            Destroy(GetComponent<SpriteRenderer>());
            Destroy(feet);

            audioSource.PlayOneShot(bloodSpatterSound[Random.Range(0, bloodSpatterSound.Length)]);

            Invoke("ToggleFailedScreen", 0.5f);
        }
    }

    void ToggleFailedScreen()
    {
        Destroy(gameObject);

        gameManager.GetComponent<GameManager>().ToggleFailedScreen();
    }

    public void SetCurrentWeapon(WeaponType newWeapon, int ammo)
    {
        anim.SetInteger("WeaponSwap", (int)newWeapon);
        animFeet.SetInteger("WeaponSwap", (int)newWeapon);


        if (newWeapon == WeaponType.Knife)
        {
            GetComponent<PlayerWeapon>().fireRate = GlobalValues.fireRateMelee;
            GetComponent<PlayerWeapon>().ammoCount = 0;
            GetComponent<PlayerWeapon>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Pistol)
        {
            GetComponent<PlayerWeapon>().fireRate = GlobalValues.fireRatePistol;
            GetComponent<PlayerWeapon>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Rifle)
        {
            GetComponent<PlayerWeapon>().fireRate = GlobalValues.fireRateRifle;
            GetComponent<PlayerWeapon>().currentWeapon = newWeapon;
        }
        else if (newWeapon == WeaponType.Shotgun)
        {
            GetComponent<PlayerWeapon>().fireRate = GlobalValues.fireRateShotgun;
            GetComponent<PlayerWeapon>().currentWeapon = newWeapon;
        }


        GetComponent<PlayerWeapon>().ammoCount = ammo;
    }

}
