using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 50f;
    public GameObject Mass;
    private Vector3 MassPoint;
    private Rigidbody rb;
    float rotation = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.InverseTransformPoint(MassPoint);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) // При нажатии клавиши A
        {
            rotation = 1;
            rb.AddTorque(transform.forward * rotation * rotationSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.D)) // При нажатии клавиши D
        {
            rotation = -1;
            rb.AddTorque(transform.forward * rotation * rotationSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }

        // Применяем вращение
    }
}
