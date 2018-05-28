using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletSpawn;
    public GameObject bullet;
    public GameObject bulletNoiseRadius;
    public GameObject meleeHitBox;

    public AudioClip pistolSound;
    public AudioClip shotgunSound;
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

    private void Start()
    {
        weaponSound = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        bulletAnim = bulletSpawn.GetComponent<Animator>();

        playerHandler = GetComponentInParent<PlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // 0 = Knife
        if (playerHandler.currentWeapon == 0)
        {
            if (Input.GetButton("Fire1") && (Time.realtimeSinceStartup - lastFired > fireRate))
            {
                anim.SetTrigger("Shoot");
                lastFired = Time.realtimeSinceStartup;
                meleeHitBox.GetComponent<EnemyTracker>().DestroyEnemies();
            }
        }
        // 1 = Pistol, 2 = Rifle
        else if (playerHandler.currentWeapon == 1 || playerHandler.currentWeapon == 2)
        {
            if (Input.GetButton("Fire1") && (Time.realtimeSinceStartup - lastFired > fireRate) && ammoCount > 0)
            {
                weaponSound.pitch = Random.Range(0.8f, 1.3f);
                weaponSound.PlayOneShot(pistolSound);

                bulletInstance = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bulletNoiseRadius.GetComponent<EnemyTracker>().NotifyEnemies();

                anim.SetTrigger("Shoot");
                bulletAnim.SetTrigger("Shoot");
                lastFired = Time.realtimeSinceStartup;
                ammoCount--;

                if (ammoCount == 0)
                {
                    playerHandler.SetCurrentWeapon(0);
                }
            }
        }
        // 3 = Shotgun
        else if (playerHandler.currentWeapon == 3)
        {
            if (Input.GetButton("Fire1") && (Time.realtimeSinceStartup - lastFired > fireRate) && ammoCount > 0)
            {
                weaponSound.pitch = Random.Range(0.8f, 1.3f);
                weaponSound.PlayOneShot(shotgunSound);

                for (int i = 0; i < shotgunPellets; i++)
                {
                    bulletInstance = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                    bulletInstance.GetComponent<Transform>().Rotate(0, 0, Random.Range(-1 * GlobalValues.spreadShotgun, GlobalValues.spreadShotgun));
                }

                bulletNoiseRadius.GetComponent<EnemyTracker>().NotifyEnemies();

                anim.SetTrigger("Shoot");
                bulletAnim.SetTrigger("Shoot");
                lastFired = Time.realtimeSinceStartup;
                ammoCount--;

                if (ammoCount == 0)
                {
                    playerHandler.SetCurrentWeapon(0);
                }
            }
        }
    }
}
