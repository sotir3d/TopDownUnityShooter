using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletTransform;
    public GameObject bullet;
    public GameObject bulletNoiseRadius;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
            bulletNoiseRadius.GetComponent<BulletNoise>().notifyEnemies();
        }
    }
}
