using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform playerTransform;

    Vector3 cameraPosition;
    Vector3 mouseDistanceFromPlayer;

    float maxCameraOffset = 2;
    float cameraMoveSpeed = 10f;
    float minCursorDistance = 0.1f;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        cameraPosition = transform.position;
        cameraPosition.x = playerTransform.position.x;
        cameraPosition.y = playerTransform.position.y;
        transform.position = cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        mouseDistanceFromPlayer = Vector3.ClampMagnitude(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) - playerTransform.position, maxCameraOffset);

        cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerTransform.position.x + mouseDistanceFromPlayer.x, Time.smoothDeltaTime * cameraMoveSpeed);
        cameraPosition.y = Mathf.Lerp(cameraPosition.y, playerTransform.position.y + mouseDistanceFromPlayer.y, Time.smoothDeltaTime * cameraMoveSpeed);

        //if (mouseDistanceFromPlayer.x > minCursorDistance)
        //{
        //    cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerTransform.position.x + maxCameraOffset, cameraMoveSpeed);
        //}
        //else if (mouseDistanceFromPlayer.x < -minCursorDistance)
        //{
        //    cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerTransform.position.x - maxCameraOffset, cameraMoveSpeed);
        //}
        //else
        //{
        //    cameraPosition.x = Mathf.Lerp(cameraPosition.x, playerTransform.position.x, cameraMoveSpeed);
        //}

        //if (mouseDistanceFromPlayer.y > minCursorDistance)
        //{
        //    cameraPosition.y = Mathf.Lerp(cameraPosition.y, playerTransform.position.y + maxCameraOffset, cameraMoveSpeed);
        //}
        //else if (mouseDistanceFromPlayer.y < -minCursorDistance)
        //{
        //    cameraPosition.y = Mathf.Lerp(cameraPosition.y, playerTransform.position.y - maxCameraOffset, cameraMoveSpeed);
        //}
        //else
        //{
        //    cameraPosition.y = Mathf.Lerp(cameraPosition.y, playerTransform.position.y, cameraMoveSpeed);
        //}

        transform.position = cameraPosition;
    }
}
