using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletSpawn;
    public GameObject bullet;
    public GameObject bulletNoiseRadius;
    public GameObject meleeHitBox;

    public GameObject pistolPickup;
    public GameObject riflePickup;
    public GameObject shotgunPickup;

    public AudioClip pistolSound;
    public AudioClip rifleSound;
    public AudioClip shotgunSound;
    public AudioClip emptyGunSound;
    public AudioClip knifeSwingSound;

    public WeaponType currentWeapon = WeaponType.Knife;

    public float shotgunPellets = 10;

    public float fireRate = GlobalValues.fireRateMelee;
    public int ammoCount = 0;

    public GameObject muzzleLight;


    AudioSource weaponSound;

    Animator anim;
    Animator bulletAnim;
    PlayerHandler playerHandler;

    AudioSource currentGunSound;

    GameObject bulletInstance;

    float lastFired = 0;
    float spreadRifle = 0;
    float spreadRifleIncrease = 0.3f;
    float lastMuzzleFlash = 0;

    private void Start()
    {
        weaponSound = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        bulletAnim = bulletSpawn.GetComponent<Animator>();

        playerHandler = GetComponentInParent<PlayerHandler>();
        //playerHandler.SetCurrentWeapon(currentWeapon, 30);
    }
    
    // Update is called once per frame
    void Update()
    {
        //ignores input when game is paused
        if (Time.timeScale == 0)
            return;
        
        if (Input.GetButton("Fire1") && (Time.time - lastFired > fireRate))
        {
            FireAShot();
        }

        //resets the spread for the rifle, when it is no longer fired
        if (!Input.GetButton("Fire1"))
        {
            spreadRifle = 0;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            ThrowWeapon(currentWeapon);
        }

        if(Time.time - lastMuzzleFlash > 0.05f)
            muzzleLight.GetComponent<Light>().enabled = false;
    }

    void ThrowWeapon(WeaponType currentThrownWeapon)
    {
        GameObject thrownWeapon;

        if(GetPickupFromWeapontype(currentThrownWeapon) != null)
        {
            thrownWeapon = Instantiate(GetPickupFromWeapontype(currentThrownWeapon), bulletSpawn.transform.position, transform.rotation);
            thrownWeapon.GetComponent<PickupScript>().ThrowWeapon();
            thrownWeapon.GetComponent<PickupScript>().player = gameObject;
            thrownWeapon.GetComponent<PickupScript>().ammo = ammoCount;
            playerHandler.SetCurrentWeapon(WeaponType.Knife, 0);
        }
    }

    public void DropCurrentWeapon()
    {
        GameObject droppedWeapon;
        if (GetPickupFromWeapontype(currentWeapon) != null)
        {
            droppedWeapon = Instantiate(GetPickupFromWeapontype(currentWeapon), transform.position, transform.rotation);
            droppedWeapon.GetComponent<PickupScript>().player = gameObject;
            droppedWeapon.GetComponent<PickupScript>().ammo = ammoCount;
        }
    }

    GameObject GetPickupFromWeapontype(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Pistol)
        {
            return pistolPickup;
        }
        else if (weaponType == WeaponType.Rifle)
        {
            return riflePickup;
        }
        else if (weaponType == WeaponType.Shotgun)
        {
            return shotgunPickup;
        }
        else
        {
            return null;
        }
    }

    void FireAShot()
    {
        weaponSound.pitch = Random.Range(0.8f, 1.3f);

        if (currentWeapon == WeaponType.Knife)
        {
            meleeHitBox.GetComponent<EnemyTracker>().DestroyEnemies();
        }
        else if ((currentWeapon == WeaponType.Pistol || currentWeapon == WeaponType.Rifle) && ammoCount > 0)
        {
            bulletInstance = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

            if (currentWeapon == WeaponType.Rifle)
            {
                bulletInstance.GetComponent<Transform>().Rotate(0, 0, Random.Range(-1 * spreadRifle, spreadRifle));

                spreadRifle += spreadRifleIncrease;
            }
        }
        else if (currentWeapon == WeaponType.Shotgun && ammoCount > 0)
        {
            for (int i = 0; i < shotgunPellets; i++)
            {
                bulletInstance = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bulletInstance.GetComponent<Transform>().Rotate(0, 0, Random.Range(-1 * GlobalValues.spreadShotgun, GlobalValues.spreadShotgun));
            }
        }



        anim.SetTrigger("Shoot");

        lastFired = Time.time;

        if (currentWeapon != WeaponType.Knife)
        {
            if (ammoCount <= 0)
            {
                //playerHandler.SetCurrentWeapon(0,0);
                weaponSound.PlayOneShot(emptyGunSound);
            }

            else if (ammoCount > 0)
            {
                muzzleLight.GetComponent<Light>().enabled = true;

                lastMuzzleFlash = Time.time;

                ammoCount--;
                bulletAnim.SetTrigger("Shoot");

                if (currentWeapon == WeaponType.Pistol)
                    weaponSound.PlayOneShot(pistolSound);
                else if (currentWeapon == WeaponType.Rifle)
                    weaponSound.PlayOneShot(rifleSound);
                else if (currentWeapon == WeaponType.Shotgun)
                    weaponSound.PlayOneShot(shotgunSound);

                bulletNoiseRadius.GetComponent<EnemyTracker>().NotifyEnemies();
            }
        }
        else if (currentWeapon == WeaponType.Knife)
        {
            weaponSound.PlayOneShot(knifeSwingSound);
        }
    }
}
