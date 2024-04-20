using UnityEngine;

public class RotateSphere : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public Vector3 rotationAxis = Vector3.up;

    void Update()
    {
        Quaternion rotation = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, rotationAxis);
        transform.rotation = rotation * transform.rotation; // Apply the rotation
    }
}
