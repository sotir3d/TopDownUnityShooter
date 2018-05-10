using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletLeftTransform;
    public Transform bulletRightTransform;
    public GameObject bullet;

    bool fireLeft = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (fireLeft)
            {
                Instantiate(bullet, bulletLeftTransform.position, bulletLeftTransform.rotation);
            }
            else
            {
                Instantiate(bullet, bulletRightTransform.position, bulletRightTransform.rotation);
            }
            fireLeft = !fireLeft;
        }
    }
}
