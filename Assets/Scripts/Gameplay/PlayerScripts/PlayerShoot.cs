using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletTransform;
    public GameObject bullet;
    public GameObject bulletNoiseRadius;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
            Instantiate(bullet, bulletTransform.position, bulletTransform.rotation);
            bulletNoiseRadius.GetComponent<BulletNoise>().notifyEnemies();
        }
    }
}
