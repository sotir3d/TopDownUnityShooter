using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    Animator anim;

    public int currentWeapon;

    public GameObject gameManager;


    private void Start()
    {
        anim = GetComponent<Animator>();

        currentWeapon = 0;
    }

    private void Update()
    {
        Debug.Log(gameManager.GetComponent<GameManager>().enemyCount);
        if (gameManager.GetComponent<GameManager>().enemyCount == 0)
        {
            gameManager.GetComponent<GameManager>().ToggleVictoryScreen();
        }

        anim.SetInteger("WeaponSwap", currentWeapon);
    }

    public void Death()
    {
        //GameplayHandler.enemyCount = 0;
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
    
    public void SetCurrentWeapon(int newWeapon)
    {
        currentWeapon = newWeapon;

        if(newWeapon == 0)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRatePistol;
        }
        else if (newWeapon == 1)
        {
            GetComponent<PlayerShoot>().fireRate = GlobalValues.fireRateRifle;
        }
    }

}
