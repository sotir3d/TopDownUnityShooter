using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject feet;
    public AudioClip footstepSound;

    Vector2 direction;
    Quaternion playerRotation;
    Rigidbody2D playerRigidbody;

    Animator anim;
    Animator feetAnim;

    AudioSource playerAudioSource;

    float angle;
    float horizontalInput;
    float verticalInput;

    float lastFootstepSound;
    float footstepSoundSpeed = 0.6f;

    // Use this for initialization
    void Start()
    {
        playerRotation = transform.rotation;
        playerRigidbody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        feetAnim = feet.GetComponent<Animator>();

        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * moveSpeed;

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = playerRotation;

        if (Mathf.Abs(playerRigidbody.velocity.x) > 0 || Mathf.Abs(playerRigidbody.velocity.y) > 0)
        {
            anim.SetFloat("Speed", 1);

            //SetFeetRotation();
            feetAnim.SetFloat("Speed", 1);

            if((Time.time - lastFootstepSound) > footstepSoundSpeed)
            {
                playerAudioSource.PlayOneShot(footstepSound);
                lastFootstepSound = Time.time;
            }
        }
        else
        {
            playerRigidbody.velocity = new Vector3(0, 0, 0);
            anim.SetFloat("Speed", 0);
            feetAnim.SetFloat("Speed", 0);
        }

    }

    void SetFeetRotation()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Up
        if (horizontalInput == 0 && verticalInput == 1)
        {
            feet.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        // Up-Right
        else if (horizontalInput == 1 && verticalInput == 1)
        {
            Debug.Log(verticalInput);
            feet.transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        //Right
        else if (horizontalInput == 1 && verticalInput == 0)
        {
            feet.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //Down-Right
        else if (horizontalInput == 1 && verticalInput == -1)
        {
            feet.transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        //Down
        else if (horizontalInput == 0 && verticalInput == -1)
        {
            feet.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        //Down-Left
        else if (horizontalInput == -1 && verticalInput == -1)
        {
            feet.transform.rotation = Quaternion.Euler(0, 0, 225);
        }
        //Left
        else if (horizontalInput == -1 && verticalInput == 0)
        {
            feet.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        //Up-Left
        else if (horizontalInput == -1 && verticalInput == 1)
        {
            feet.transform.rotation = Quaternion.Euler(0, 0, 135);
        }
    }
}
