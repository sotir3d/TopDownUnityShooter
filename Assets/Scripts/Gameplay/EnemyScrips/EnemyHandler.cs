﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject enemyCharacter;
    public GameObject bloodSpatter;
    public GameObject bloodDecal;

    public AudioClip[] bloodSpatterSound;

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
            
            Instantiate(bloodSpatter, enemyCharacter.transform.position, enemyCharacter.transform.rotation);
            Instantiate(bloodDecal, enemyCharacter.transform.position, enemyCharacter.transform.rotation);

            gameManager.GetComponent<GameManager>().enemyCount--;
            Destroy(enemyCharacter);
            
            enemyAudioSource.PlayOneShot(bloodSpatterSound[Random.Range(0,bloodSpatterSound.Length)]);
            Invoke("DestroyObject", 0.5f);
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
