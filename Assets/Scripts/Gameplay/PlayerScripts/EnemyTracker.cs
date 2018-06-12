using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> iterateEnemies = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        iterateEnemies = enemies;
        // deletes dead enemies out of the list
        for (int i = 0; i < iterateEnemies.Count; i++)
        {
            if (iterateEnemies[i] == null)
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

    public void NotifyEnemies()
    {
        int i = 0;
        foreach (var enemy in enemies)
        {
            i++;
            if(enemy != null)
                enemy.GetComponent<EnemyPatrolling>().CheckSightlineToPlayer();
        }
    }

    public void DestroyEnemies()
    {
        for (int i = 0; i < iterateEnemies.Count; i++)
        {
            enemies[i].GetComponentInParent<EnemyHandler>().Death();
        }
    }
}
