﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float turnSpeedWalk = 5;
    public float turnSpeedRun = 30;

    public Transform playerTransform;

    public bool isSeeingPlayer = false;

    public float enemySpeedWalk = 2;
    public float enemySpeedRun = 5;

    public AudioClip[] attackSound;
    Animator anim;

    AudioSource zombieAudioSource;
	
	
	
    float enemySpeed;
    float turnSpeed = 5;
    Vector3 patrolPointDir;

    Transform currentPatrolPoint;
    int currentPatrolIndex;

    float lastTimeSeenPlayer = 0;

    float minDistanceToPatrolpoint = 0.1f;
    float angle;
    Quaternion q;

    float maxLastSeenTime = 2;

    bool isScreaming = false;

    // Use this for initialization
    void Start()
    {
        enemySpeed = enemySpeedWalk;

        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];

        anim = GetComponent<Animator>();

        zombieAudioSource = GetComponent<AudioSource>();
        zombieAudioSource.Play();
        
        //randomize the idle animation between zombies
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);//could replace 0 by any other animation layer index

        anim.Play(state.fullPathHash, -1, Random.Range(0f, 4f));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTransform == null)
            return;

        if ((Time.time - lastTimeSeenPlayer) > maxLastSeenTime && isSeeingPlayer == true)
        {
            // when losing sight to player, move back towards the last targeted patrol point
            enemySpeed = enemySpeedWalk;
            turnSpeed = turnSpeedWalk;

            isSeeingPlayer = false;
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
            isScreaming = false;

        }

        if (isSeeingPlayer == false)
        {
            if (Vector3.Distance(transform.position, currentPatrolPoint.position) < minDistanceToPatrolpoint)
            {
                if (currentPatrolIndex + 1 < patrolPoints.Length)
                {
                    currentPatrolIndex++;
                }
                else
                {
                    currentPatrolIndex = 0;
                }
                currentPatrolPoint = patrolPoints[currentPatrolIndex];
            }
        }

        if (isSeeingPlayer == true)
        {
            currentPatrolPoint = playerTransform;
            enemySpeed = enemySpeedRun;
            turnSpeed = turnSpeedRun;
        }

        if (currentPatrolPoint != null)
        {
            if (Vector3.Distance(currentPatrolPoint.position, transform.position) > 0.1f)
            {
                // get the angle between enemy and the next patrol point
                patrolPointDir = currentPatrolPoint.position - transform.position;
                angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg;

                // rotate the enemy toward the next patrol point, after rotation is finished, move towards patrol point
                q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, turnSpeed);

                if (transform.rotation == q)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * enemySpeed);
                    anim.SetBool("isMoving", true);
                }

                anim.SetBool("isMoving", true);
                //destinationSetter.target = currentPatrolPoint;
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }


    }

    public void CheckSightlineToPlayer()
    {
        int layerMask;

        // set up the layer mask, so that it ignores layers 9, 10 and 11
        layerMask = 1 << 9;
        layerMask += 1 << 10;
        layerMask += 1 << 11;
        layerMask = ~layerMask;

        if(playerTransform != null)
        {
            //racast from the enemy towards the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, Mathf.Infinity, layerMask);

            if (hit.collider.gameObject.tag == "Player")
            {
                SeesPlayer();
            }
        }
    }

    public void SeesPlayer()
    {
        lastTimeSeenPlayer = Time.time;
        isSeeingPlayer = true;

        //avoid multiple screams when charging
        if(!isScreaming)
        {
            zombieAudioSource.pitch = Random.Range(0.8f, 1.2f);
            zombieAudioSource.PlayOneShot(attackSound[Random.Range(0,attackSound.Length)]);
            isScreaming = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHandler>().Death();
        }
    }
}