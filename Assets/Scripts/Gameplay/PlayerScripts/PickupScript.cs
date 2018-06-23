using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{

    public GameObject player;

    new Light light;

    public WeaponType weaponType;
    public AudioClip throwSound;
    public AudioClip pickupSound;
    public AudioClip hitSound;

    public int ammo;

    float rotationSpeed = 30;

    AudioSource pickupAudioSource;

    float pickupDelay = 0.5f;
    float pickupSpawnedTime;

    float throwSpeed = 20;
    float currentRotationSpeed = 0;
    bool currentlyThrowing;

    // Use this for initialization
    void Start()
    {
        light = GetComponentInChildren<Light>();
        pickupAudioSource = GetComponent<AudioSource>();
        pickupSpawnedTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentlyThrowing)
            transform.Rotate(0, 0, currentRotationSpeed);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //avoids instantly picking up the pickup again, if it was dropped
        if ((Time.time - pickupSpawnedTime) < pickupDelay)
            return;
        
        if (Input.GetButton("Fire2") && collision.gameObject.tag == "Player")
        {
            
            player.GetComponent<PlayerWeapon>().DropCurrentWeapon();
            player.GetComponent<PlayerHandler>().SetCurrentWeapon(weaponType, ammo);
            pickupAudioSource.PlayOneShot(pickupSound);

            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(light);
            Invoke("DestroyPickup", 1);
        }
    }

    void DestroyPickup()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && currentlyThrowing)
        {
            pickupAudioSource.PlayOneShot(hitSound);
            collision.gameObject.GetComponentInParent<EnemyHandler>().Death();
        }
    }

    public void ThrowWeapon()
    {
        pickupAudioSource = GetComponent<AudioSource>();
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(throwSpeed, 0));
        currentRotationSpeed = rotationSpeed;
        currentlyThrowing = true;

        //The pickup is set to collision while throwing, to avoid clipping through walls
        GetComponent<BoxCollider2D>().isTrigger = false;

        pickupAudioSource.PlayOneShot(throwSound);
        Invoke("StopThrow", 0.3f);
    }

    void StopThrow()
    {
        currentRotationSpeed = 0;
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(0, 0));

        //set the pickup to be a trigger, after being thrown
        GetComponent<BoxCollider2D>().isTrigger = true;
        currentlyThrowing = false;
    }
}

