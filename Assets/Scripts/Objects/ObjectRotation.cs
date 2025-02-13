using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 30f; // Speed of rotation in degrees per second
    public Vector3 rotationAxis = Vector3.up; // Axis to rotate around (e.g., Vector3.up for Y-axis)
    void Update()
    {
          // Rotate the object around the specified axis
          transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}

