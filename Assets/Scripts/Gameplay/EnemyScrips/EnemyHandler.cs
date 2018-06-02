using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject enemyCharacter;
    public GameObject bloodSpatter;

    public AudioClip explosionSound1;
    public AudioClip explosionSound2;

    AudioSource enemyAudioSource;

    bool deathTriggered = false;

    public void Start()
    {
        gameManager.GetComponent<GameManager>().enemyCount++;
        enemyAudioSource = GetComponent<AudioSource>();
    }

    public void Death()
    {
        int randNum = Random.Range(1, 3);
        //prevents triggering through multiple shotgun hits on the same frame
        if(!deathTriggered)
        {
            deathTriggered = true;
            Transform spatterTransform = enemyCharacter.transform;
            spatterTransform.Rotate(0, 0, 180);
            Instantiate(bloodSpatter, enemyCharacter.transform.position, spatterTransform.rotation);
            gameManager.GetComponent<GameManager>().enemyCount--;
            Destroy(enemyCharacter);
            if(randNum == 1)
            {
                enemyAudioSource.PlayOneShot(explosionSound1);
            }
            else
            {
                enemyAudioSource.PlayOneShot(explosionSound2);
            }
            Invoke("DestroyObject", 0.5f);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
