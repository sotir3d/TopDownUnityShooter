using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpatterScript : MonoBehaviour
{
    public Sprite[] bloodDecals;

    SpriteRenderer spriteRenderer;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = bloodDecals[Random.Range(0,bloodDecals.Length)];
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360f)));
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
