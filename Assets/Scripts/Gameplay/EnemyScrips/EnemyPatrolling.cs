using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float turnSpeed = 5;

    public Transform playerTransform;

    public bool isSeeingPlayer = false;

    public float enemySpeedWalk = 2;
    public float enemySpeedRun = 5;



    float enemySpeed;
    Vector3 patrolPointDir;

    Transform currentPatrolPoint;
    int currentPatrolIndex;

    float lastTimeSeenPlayer = 0;

    float minDistanceToPatrolpoint = 0.1f;

    // Use this for initialization
    void Start()
    {
        enemySpeed = enemySpeedWalk;
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void Update()
    {
        float angle;
        Quaternion q;

        if ((Time.realtimeSinceStartup - lastTimeSeenPlayer) > 2)
        {
            // when losing sight to player, move back towards the last targeted patrol point
            enemySpeed = enemySpeedWalk;
            isSeeingPlayer = false;
            currentPatrolPoint = patrolPoints[currentPatrolIndex];

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
        }


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
            }
        }


    }

    public void CheckSightlineToPlayer()
    {
        int layerMask;

        // set up the layer mask, so that it ignores layers 9 and 10
        layerMask = 1 << 9;
        layerMask += 1 << 10;
        layerMask += 1 << 11;
        layerMask = ~layerMask;
        

        // racast from the enemy towards the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, Mathf.Infinity, layerMask);
        Debug.Log((Time.realtimeSinceStartup - lastTimeSeenPlayer));
        if (hit.collider.gameObject.tag == "Player")
        {
            lastTimeSeenPlayer = Time.realtimeSinceStartup;
            isSeeingPlayer = true;
        }
        //else if((Time.realtimeSinceStartup - lastSeen) > 2)
        //{
        //    // when losing sight to player, move back towards the last targeted patrol point
        //    enemySpeed = enemySpeedWalk;
        //    isSeeingPlayer = false;
        //    currentPatrolPoint = patrolPoints[currentPatrolIndex];
            
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHandler>().Death();
        }
    }
}
