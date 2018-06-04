using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
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
    public WeaponType currentWeapon = WeaponType.Knife;
    AudioSource weaponSound;

    public float shotgunPellets = 10;

    public float fireRate = GlobalValues.fireRateMelee;
    public int ammoCount = 0;

    Animator anim;
    Animator bulletAnim;
    PlayerHandler playerHandler;

    AudioSource currentGunSound;

    GameObject bulletInstance;

    float lastFired = 0;
    float spreadRifle = 0;
    float spreadRifleIncrease = 0.3f;

    private void Start()
    {
        weaponSound = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        bulletAnim = bulletSpawn.GetComponent<Animator>();

        playerHandler = GetComponentInParent<PlayerHandler>();
        //playerHandler.SetCurrentWeapon(currentWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && (Time.realtimeSinceStartup - lastFired > fireRate))
        {
            FireAShot();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            if (currentWeapon == WeaponType.Pistol)
            {
                ThrowWeapon(pistolPickup);
            }
            else if (currentWeapon == WeaponType.Rifle)
            {
                ThrowWeapon(riflePickup);
            }
            else if (currentWeapon == WeaponType.Shotgun)
            {
                ThrowWeapon(shotgunPickup);
            }
        }
    }

    void ThrowWeapon(GameObject currentThrownWeapon)
    {
        GameObject thrownWeapon;
        playerHandler.SetCurrentWeapon(0);
        thrownWeapon = Instantiate(currentThrownWeapon, bulletSpawn.transform.position, transform.rotation);
        thrownWeapon.GetComponent<PickupScript>().ThrowWeapon();
        thrownWeapon.GetComponent<PickupScript>().player = gameObject;
    }

    void FireAShot()
    {
        weaponSound.pitch = Random.Range(0.8f, 1.3f);

        if (currentWeapon == WeaponType.Pistol)
            weaponSound.PlayOneShot(pistolSound);
        else if (currentWeapon == WeaponType.Rifle)
            weaponSound.PlayOneShot(rifleSound);
        else if (currentWeapon == WeaponType.Shotgun)
            weaponSound.PlayOneShot(shotgunSound);


        if (currentWeapon == WeaponType.Knife)
        {
            meleeHitBox.GetComponent<EnemyTracker>().DestroyEnemies();
        }
        else if (currentWeapon == WeaponType.Pistol || currentWeapon == WeaponType.Rifle && ammoCount > 0)
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


        bulletNoiseRadius.GetComponent<EnemyTracker>().NotifyEnemies();

        anim.SetTrigger("Shoot");

        lastFired = Time.realtimeSinceStartup;

        if (currentWeapon != WeaponType.Knife)
        {
            ammoCount--;
            bulletAnim.SetTrigger("Shoot");

            if (ammoCount == 0)
            {
                playerHandler.SetCurrentWeapon(0);
            }
        }
    }
}
