using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(speed, 0));
		Invoke("Destroy", 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

	void Destroy()
	{
		Destroy(gameObject);
	}
}
