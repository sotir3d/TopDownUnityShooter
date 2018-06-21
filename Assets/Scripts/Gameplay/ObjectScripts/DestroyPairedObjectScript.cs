using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPairedObjectScript : MonoBehaviour
{
    public GameObject pairedObject;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pairedObject == null)
            Destroy(gameObject);
    }
}
