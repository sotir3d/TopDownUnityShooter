using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(speed, 0));
		Invoke("Destroy", 3);
    }

	void Destroy()
	{
		Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponentInParent<EnemyHandler>().Death();
        }

        if (collision.gameObject.tag != "Projectile")
        {
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
            Invoke("DestroyBullet", 0.2f);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
