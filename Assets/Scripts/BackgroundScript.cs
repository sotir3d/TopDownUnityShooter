using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    float lastColorChange;
    // Use this for initialization
    void Start()
    {
        lastColorChange = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.realtimeSinceStartup - lastColorChange > 1.5f)
        //{
        //    GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);

        //    lastColorChange = Time.realtimeSinceStartup;
        //}
    }
}
