using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Camera camera;
    private void Start()
    {
        camera = GetComponent<Camera>();
    }
    private void Update()
    {
    }
}
