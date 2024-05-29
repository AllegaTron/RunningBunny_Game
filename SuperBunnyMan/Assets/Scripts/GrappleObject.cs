using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleObject : MonoBehaviour
{
    private Rigidbody rb;
    public LayerMask grappleLayers; // ������� ����, � �������� ����� ���������
    private Vector3 grapplePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��� ������� ���
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, grappleLayers))
            {
                grapplePoint = hit.point; // ����� ���������������
                rb.isKinematic = true; // ���������� ������ �������
            }
        }

        if (Input.GetMouseButtonUp(0)) // ��� ���������� ���
        {
            rb.isKinematic = false; // ����������� ������ �������
        }
    }

    void FixedUpdate()
    {
        if (rb.isKinematic)
        {
            rb.MovePosition(grapplePoint); // ���������� ������ � ����� ���������������
            rb.centerOfMass = transform.InverseTransformPoint(grapplePoint); // ������������� ����� ������� � ����� ���������������
        }
    }
}
