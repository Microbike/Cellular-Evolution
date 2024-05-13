using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public Vector3 offset;

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, moveSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        Quaternion desiredRotation = target.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
    }
}
