using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNoise : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collision2D collision)
    //{
    //    Debug.Log(collision.gameObject);
    //    //enemies.Add(collision.gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemies.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemies.Remove(collision.gameObject);
    }

    public void notifyEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyPatrolling>().CheckSightlineToPlayer();
        }
    }
}
