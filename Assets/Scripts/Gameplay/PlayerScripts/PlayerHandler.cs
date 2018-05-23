using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    Animator anim;

    public int weapon;

    public GameObject gameManager;


    private void Start()
    {
        anim = GetComponent<Animator>();

        anim.SetInteger("WeaponSwap", weapon);
    }

    private void Update()
    {
        Debug.Log(gameManager.GetComponent<GameManager>().enemyCount);
        if (gameManager.GetComponent<GameManager>().enemyCount == 0)
        {
            Debug.Log("Victory");
        }
    }

    public void Death()
    {
        //GameplayHandler.enemyCount = 0;
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }


}
