using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletSpawn;
    public GameObject bullet;
    public GameObject bulletNoiseRadius;
    public AudioSource gunShot;

    public float fireRate = GlobalValues.fireRateMelee;
    public int ammoCount = 0;

    Animator anim;
    Animator bulletAnim;
    PlayerHandler playerHandler;

    float lastFired = 0;

    private void Start()
    {
        gunShot = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        bulletAnim = bulletSpawn.GetComponent<Animator>();

        playerHandler = GetComponentInParent<PlayerHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHandler.currentWeapon == 0)
        {
            if (Input.GetButton("Fire1") && (Time.realtimeSinceStartup - lastFired > fireRate))
            {
                anim.SetTrigger("Shoot");
                lastFired = Time.realtimeSinceStartup;
            }
        }
        else if (playerHandler.currentWeapon == 1 || playerHandler.currentWeapon == 2)
        {
            if (Input.GetButton("Fire1") && (Time.realtimeSinceStartup - lastFired > fireRate) && ammoCount > 0)
            {
                gunShot.pitch = Random.Range(0.8f, 1.3f);
                gunShot.Play();
                Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
                bulletNoiseRadius.GetComponent<BulletNoise>().notifyEnemies();

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
