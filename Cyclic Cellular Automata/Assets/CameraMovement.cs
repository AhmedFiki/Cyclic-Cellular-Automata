using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 10f;  // Speed of camera movement
    public float zoomSpeed = 1f;      // Speed of camera zooming


    void Update()
    {
        // Camera movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Camera zooming
        // Zooming
        float scrollDelta = Input.mouseScrollDelta.y;
        Zoom(scrollDelta);

    }
    private void Zoom(float delta)
    {
        float zoomAmount = delta * zoomSpeed;
        GetComponent<Camera>().orthographicSize -= zoomAmount;
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, 1f, Mathf.Infinity);
    }
}
