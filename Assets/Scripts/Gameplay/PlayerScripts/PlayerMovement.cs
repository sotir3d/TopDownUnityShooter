using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    Vector2 direction;
    Quaternion playerRotation;
    Rigidbody2D playerRigidbody;

    Animator anim;

    float angle;

    // Use this for initialization
    void Start()
    {
        playerRotation = transform.rotation;
        playerRigidbody = GetComponent<Rigidbody2D>();

        playerRotation.z = 90;
        transform.rotation = playerRotation;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * moveSpeed;

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = playerRotation;

        if (Mathf.Abs(playerRigidbody.velocity.x) > 0 || Mathf.Abs(playerRigidbody.velocity.y) > 0)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            playerRigidbody.velocity = new Vector3(0, 0, 0);
            anim.SetFloat("Speed", 0);
        }

    }
}
