using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPatrolling : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float turnSpeedWalk = 5;
    public float turnSpeedRun = 180;

    public Transform playerTransform;

    public bool isSeeingPlayer = false;

    public float enemySpeedWalk = 2;
    public float enemySpeedRun = 5;

    Animator anim;

    AIDestinationSetter destinationSetter;
    AILerp aiLerp;

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

    // Use this for initialization
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiLerp = GetComponent<AILerp>();

        enemySpeed = enemySpeedWalk;
        
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];

        anim = GetComponent<Animator>();

        
        aiLerp.speed = enemySpeedWalk;
        
        //randomize the idle animation between zombies
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);//could replace 0 by any other animation layer index

        anim.Play(state.fullPathHash, -1, Random.Range(0f, 4f));
    }

    // Update is called once per frame
    void Update()
    {

        if ((Time.realtimeSinceStartup - lastTimeSeenPlayer) > maxLastSeenTime && isSeeingPlayer == true)
        {
            // when losing sight to player, move back towards the last targeted patrol point
            aiLerp.speed = enemySpeedWalk;
            aiLerp.repathRate = 0.5f;
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
            turnSpeed = turnSpeedRun;
            aiLerp.speed = enemySpeedRun;
            aiLerp.repathRate = 0;
        }

        if (currentPatrolPoint != null)
        {
            if (Vector3.Distance(currentPatrolPoint.position, transform.position) > 0.1f)
            {
                // // get the angle between enemy and the next patrol point
                // patrolPointDir = currentPatrolPoint.position - transform.position;
                // angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg;

                // // rotate the enemy toward the next patrol point, after rotation is finished, move towards patrol point
                // q = Quaternion.AngleAxis(angle, Vector3.forward);
                // //transform.rotation = Quaternion.RotateTowards(transform.rotation, q, turnSpeed);
                
                // if (transform.rotation == q)
                // {
                //     //transform.Translate(Vector3.right * Time.deltaTime * enemySpeed);
                //     anim.SetBool("isMoving", true);
                // }

                anim.SetBool("isMoving", true);
                destinationSetter.target = currentPatrolPoint;
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

        // set up the layer mask, so that it ignores layers 9 and 10
        layerMask = 1 << 9;
        layerMask += 1 << 10;
        layerMask += 1 << 11;
        layerMask = ~layerMask;

        //racast from the enemy towards the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, Mathf.Infinity, layerMask);

        if (hit.collider.gameObject.tag == "Player")
        {
            SeesPlayer();
        }
    }

    public void SeesPlayer()
    {
        lastTimeSeenPlayer = Time.realtimeSinceStartup;
        isSeeingPlayer = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHandler>().Death();
        }
    }
}
