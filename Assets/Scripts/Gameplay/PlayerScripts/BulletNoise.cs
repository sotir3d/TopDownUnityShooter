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
        // deletes dead enemies out of the list
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (enemies.Contains(collision.gameObject) == false)
            {
                enemies.Add(collision.gameObject);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemies.Remove(collision.gameObject);
    }

    public void notifyEnemies()
    {
        int i = 0;
        foreach (var enemy in enemies)
        {
            i++;
            enemy.GetComponent<EnemyPatrolling>().CheckSightlineToPlayer();
        }
    }
}
