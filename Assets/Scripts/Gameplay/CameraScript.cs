using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public float maxCameraOffset = 5;
    public float lookAroundOffset = 20;
    public float cameraMoveSpeed = 8f;
    public Transform playerTransform;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    Vector3 cameraPosition;
    Vector3 mouseDistanceFromPlayer;

    float activeCameraOffset;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        cameraPosition = transform.position;
        cameraPosition.x = playerTransform.position.x;
        cameraPosition.y = playerTransform.position.y;
        transform.position = cameraPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(Input.GetButton("LookAround"))
        {
            activeCameraOffset = lookAroundOffset;
        }
        else
        {
            activeCameraOffset = maxCameraOffset;
        }

        mouseDistanceFromPlayer = Vector3.ClampMagnitude(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) - playerTransform.position, activeCameraOffset);

        cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerTransform.position.x + mouseDistanceFromPlayer.x, Time.deltaTime * cameraMoveSpeed);
        cameraPosition.y = Mathf.Lerp(cameraPosition.y, playerTransform.position.y + mouseDistanceFromPlayer.y, Time.deltaTime * cameraMoveSpeed);
        
        transform.position = cameraPosition;
    }
}
