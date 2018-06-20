using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    AudioSource audioSource;
    public AudioClip hoverSound;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnMouseDown()
    {
        Debug.Log("asd");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
