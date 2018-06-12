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
    void Update()
    {
        //ignores input when game is paused
        if (Time.timeScale == 0)
            return;

        playerRigidbody.velocity = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * moveSpeed;

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = playerRotation;

        if (Mathf.Abs(playerRigidbody.velocity.x) > 0 || Mathf.Abs(playerRigidbody.velocity.y) > 0)
        {
            anim.SetFloat("Speed", 1);
            
            if (feetAnim != null)
                feetAnim.SetFloat("Speed", 1);

            if ((Time.time - lastFootstepSound) > footstepSoundSpeed)
            {
                playerAudioSource.PlayOneShot(footstepSound);
                lastFootstepSound = Time.time;
            }
        }
        else
        {
            playerRigidbody.velocity = new Vector3(0, 0, 0);
            anim.SetFloat("Speed", 0);

            if (feetAnim != null)
                feetAnim.SetFloat("Speed", 0);
        }

    }
}
