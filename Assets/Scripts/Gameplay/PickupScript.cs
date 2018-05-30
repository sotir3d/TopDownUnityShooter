using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{

    public GameObject player;

    public int weaponValue = 1;

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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject == player && !currentlyThrowing)
        {
            player.GetComponent<PlayerHandler>().SetCurrentWeapon(weaponValue);

            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Enemy" && currentlyThrowing)
        {
            collision.gameObject.GetComponentInParent<EnemyHandler>().Death();
        }
    }

    public void ThrowWeapon()
    {
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(throwSpeed, 0));
        rotationSpeed = 20;
        currentlyThrowing = true;
        Invoke("StopThrow", 0.3f);
    }

    void StopThrow()
    {
        float currentSpeed;
        currentSpeed = throwSpeed;
        while (currentSpeed > 0.1f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 1f);
            GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(currentSpeed, 0));
        }

        rotationSpeed = 0;
        currentlyThrowing = false;
    }
}
