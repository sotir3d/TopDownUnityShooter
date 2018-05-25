using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletSpawn;
    public GameObject bullet;
    public GameObject bulletNoiseRadius;
    public AudioSource gunShot;
    public float fireRate = 0.25f;
    
    Animator anim;
    Animator bulletAnim;

    float lastFired = 0;

    private void Start()
    {
        gunShot = GetComponent<AudioSource>();

        anim = GetComponent<Animator>();
        bulletAnim = bulletSpawn.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && (Time.realtimeSinceStartup - lastFired > fireRate))
        {
            gunShot.pitch = Random.Range(0.8f, 1.3f);
            gunShot.Play();
            Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            bulletNoiseRadius.GetComponent<BulletNoise>().notifyEnemies();
            
            anim.SetTrigger("Shoot");
            bulletAnim.SetTrigger("Shoot");
            lastFired = Time.realtimeSinceStartup;
        }
    }
}
