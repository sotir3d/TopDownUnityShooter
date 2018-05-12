using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float EnemySpeed = 2;

    public Transform playerTransform;

    public bool isSeeingPlayer = false;

    Vector3 patrolPointDir;

    Transform currentPatrolPoint;
    int currentPatrolIndex;


    float minDistanceToPatrolpoint = 0.01f;

    // Use this for initialization
    void Start()
    {
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }

    // Update is called once per frame
    void Update()
    {
        float angle;
        Quaternion q;

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
            EnemySpeed = 5;
        }

        patrolPointDir = currentPatrolPoint.position - transform.position;

        angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg;

        q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 15f);

        transform.Translate(Vector3.right * Time.deltaTime * EnemySpeed);

    }

    public void CheckSightlineToPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerTransform.position);
        Debug.DrawLine(transform.position, playerTransform.position);
        
        Debug.Log(hit.collider);
        if (hit.collider.gameObject.tag == "Player")
        {
            isSeeingPlayer = true;
        }
        else
        {
            isSeeingPlayer = false;
        }
    }
}
