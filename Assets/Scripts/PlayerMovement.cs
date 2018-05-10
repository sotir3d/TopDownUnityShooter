using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    Vector3 playerPosition;
    Vector2 direction;
    Quaternion playerRotation;
    Rigidbody2D playerRigidbody;

    float angle;

    // Use this for initialization
    void Start()
    {
        playerPosition = transform.position;
        playerRotation = transform.rotation;
        playerRigidbody = GetComponent<Rigidbody2D>();

        playerRotation.z = 90;
        transform.rotation = playerRotation;
    }

    // Update is called once per frame
    void Update()
    {

        print(Input.GetAxis("Vertical"));
        playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed, 0);


        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = playerRotation;

    }
}
