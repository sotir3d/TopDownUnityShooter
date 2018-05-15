using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletTransform;
    public GameObject bullet;
    public GameObject bulletNoiseRadius;
    public AudioSource gunShot;

    private void Start()
    {
        gunShot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gunShot.pitch = Random.Range(0.8f, 1.3f);
            gunShot.Play();
            Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
            bulletNoiseRadius.GetComponent<BulletNoise>().notifyEnemies();
        }
    }
}
