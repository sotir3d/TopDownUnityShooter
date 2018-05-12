using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Transform enemyCharacter;
    public Transform playerCharacter;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(enemyCharacter.position, playerCharacter.position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    //Debug.Log("Entered");
        //    //RaycastHit2D hit = Physics2D.Raycast(enemyCharacter.position, collision.gameObject.transform.position);


        //    //if (hit.collider.gameObject.tag == "Player")
        //    //{
        //    //    enemyCharacter.GetComponent<EnemyPatrolling>().isSeeingPlayer = true;
        //    //}

        //}

        enemyCharacter.GetComponent<EnemyPatrolling>().CheckSightlineToPlayer();
    }
}
