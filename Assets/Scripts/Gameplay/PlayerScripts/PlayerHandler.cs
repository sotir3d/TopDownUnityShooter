using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    Animator anim;


    public int weapon;

    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetInteger("WeaponSwap", weapon);
    }

    public void Death()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
}
