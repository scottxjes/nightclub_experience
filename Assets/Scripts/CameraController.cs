using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotationSpeed = 1f;
    public float damping = 5f;
    public float minDistance = 1.0f;
    public float maxDistance = 4.0f;

    private float currentX = 0f;
    private float currentY = 0f;
    private Vector3 smoothVelocity;
    private Vector3 desiredPosition;

    void Start()
    {
        offset = this.transform.position - target.position;
    }

    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        currentY = Mathf.Clamp(currentY, -80f, 80f);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        desiredPosition = target.position + rotation * offset;

        // Perform camera collision detection and avoidance
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit))
        {
            // Calculate the distance from the camera to the obstacle hit
            float hitDistance = Vector3.Distance(target.position, hit.point);

            // Apply the minimum distance to prevent the camera from getting too close to the player
            float distance = Mathf.Clamp(hitDistance, minDistance, maxDistance);

            // Recalculate the desired position based on the new distance
            desiredPosition = target.position + rotation * offset.normalized * distance;
        }

        Vector3 smoothedPosition = Vector3.Lerp(this.transform.position, desiredPosition, damping * Time.deltaTime);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}

