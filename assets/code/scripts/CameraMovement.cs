using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform pivot;
    private float rotationSpeed = 120f;
    private float minAngle = 10f;  
    private float maxAngle = 80f;
    private float limit;

    private Vector3 offset;
    private float currentAngle;

    void Start()
    {
        offset = transform.position - pivot.position;

        currentAngle = Vector3.Angle(new Vector3(offset.x, 0, offset.z), offset);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            offset = Quaternion.AngleAxis(-rotationSpeed * Time.deltaTime, Vector3.up) * offset;
        }
        if (Input.GetKey(KeyCode.A))
        {
            offset = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, Vector3.up) * offset;
        }

        float verticalRotation = 0f;
        if (Input.GetKey(KeyCode.S))
        {
            verticalRotation = -rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            verticalRotation = rotationSpeed * Time.deltaTime;
        }

        if (verticalRotation != 0f)
        {
            limit = limit + verticalRotation;
            limit = Mathf.Clamp(limit, minAngle, maxAngle);
            float deltaAngle = limit - currentAngle;
            offset = Quaternion.AngleAxis(deltaAngle, transform.right) * offset;
            currentAngle = limit;
        }

        transform.position = pivot.position + offset;
        transform.LookAt(pivot);
    }
}
