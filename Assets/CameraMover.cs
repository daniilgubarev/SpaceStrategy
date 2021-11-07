using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    public float zoomSpeed = 5;
    public Camera targetCamera;
    public Vector3 targetPosition;

    void Start()
    {
        targetPosition = targetCamera.transform.position;
    }

    void Update()
    {
        targetCamera.transform.position = Vector3.Slerp(
            targetCamera.transform.position,
            targetPosition,
            Mathf.Clamp((Time.deltaTime), 0f, 1f));
    }

    public void OnScroll(InputValue input)
    {
        Vector3 mouse = input.Get<Vector3>();

        if (!Mathf.Approximately(mouse.z, 0f))
        {
            Debug.Log(mouse.z);
            Ray ray = targetCamera.ScreenPointToRay(mouse);
            ray.direction *= mouse.z;

            targetPosition = ray.GetPoint(Mathf.Abs(mouse.z) * zoomSpeed + Vector3.Distance(ray.origin, targetPosition));
        }
    }
}
