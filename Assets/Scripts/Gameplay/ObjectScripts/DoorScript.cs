using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Rigidbody2D rb;

    float killSpeed = 1;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && rb.velocity.magnitude > killSpeed)
        {
            collision.gameObject.GetComponentInParent<EnemyHandler>().Death();
        }
    }
}
