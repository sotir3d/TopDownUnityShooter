using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Transform enemyCharacter;
    public Transform playerCharacter;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyCharacter.GetComponent<EnemyPatrolling>().CheckSightlineToPlayer();
        }
    }
}
