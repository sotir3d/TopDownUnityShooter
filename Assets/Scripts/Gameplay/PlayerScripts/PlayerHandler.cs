using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public GameObject bulletSpawn;

    Animator anim;
    Animator bulletAnim;

    Rigidbody2D playerRigidbody;

    private void Start()
    {
        anim = GetComponent<Animator>();
        bulletAnim = bulletSpawn.GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Shoot");
            bulletAnim.SetTrigger("Shoot");
        }

        if (Mathf.Abs(playerRigidbody.velocity.x) > 0 || Mathf.Abs(playerRigidbody.velocity.y) > 0)
        {
            anim.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
}
