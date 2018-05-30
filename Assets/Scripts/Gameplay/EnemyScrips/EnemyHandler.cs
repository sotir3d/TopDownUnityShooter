using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject enemyCharacter;
    public GameObject bloodSpatter;

    public void Start()
    {
        gameManager.GetComponent<GameManager>().enemyCount++;
    }

    public void Death()
    {
        Instantiate(bloodSpatter, enemyCharacter.transform.position, enemyCharacter.transform.rotation);
        gameManager.GetComponent<GameManager>().enemyCount--;
        Destroy(gameObject);
    }
}
