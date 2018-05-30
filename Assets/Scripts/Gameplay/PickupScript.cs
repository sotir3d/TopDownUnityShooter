using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{

    public GameObject player;

    public WeaponType weaponType;

    float throwSpeed = 20;
    float rotationSpeed = 0;
    bool currentlyThrowing = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !currentlyThrowing)
        {
            player.GetComponent<PlayerHandler>().SetCurrentWeapon(weaponType);

            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && currentlyThrowing)
        {
            collision.gameObject.GetComponentInParent<EnemyHandler>().Death();
        }
    }

    public void ThrowWeapon()
    {
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(throwSpeed, 0));
        rotationSpeed = 20;
        currentlyThrowing = true;
        GetComponent<BoxCollider2D>().isTrigger = false;
        Invoke("StopThrow", 0.3f);
    }

    void StopThrow()
    {
        float currentSpeed;
        currentSpeed = throwSpeed;
        
        rotationSpeed = 0;

        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(0, 0));
        currentlyThrowing = false;
    }
}

