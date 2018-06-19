using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler
{

    // Use this for initialization
    void Start()
    {
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("asd");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("asasfsfd");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("asasfsfd");
    }
}
