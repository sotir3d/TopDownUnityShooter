using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public GameObject gameManager;

    public void Start()
    {
        gameManager.GetComponent<GameManager>().enemyCount++;
    }

    public void Death()
    {
        gameManager.GetComponent<GameManager>().enemyCount--;
        Destroy(gameObject);
    }
}
