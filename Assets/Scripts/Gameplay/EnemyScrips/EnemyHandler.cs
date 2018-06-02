using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject enemyCharacter;
    public GameObject bloodSpatter;

    bool deathTriggered = false;

    public void Start()
    {
        gameManager.GetComponent<GameManager>().enemyCount++;
    }

    public void Death()
    {
        //prevents triggering through multiple shotgun hits on the same frame
        if(!deathTriggered)
        {
            deathTriggered = true;
            Transform spatterTransform = enemyCharacter.transform;
            spatterTransform.Rotate(0, 0, 180);
            Instantiate(bloodSpatter, enemyCharacter.transform.position, spatterTransform.rotation);
            gameManager.GetComponent<GameManager>().enemyCount--;
            Destroy(gameObject);
        }
    }
}
