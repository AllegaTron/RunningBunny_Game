using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleObject : MonoBehaviour
{
    private Rigidbody rb;
    public LayerMask grappleLayers; // Укажите слои, с которыми можно цепляться
    private Vector3 grapplePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // При нажатии ЛКМ
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, grappleLayers))
            {
                grapplePoint = hit.point; // Точка соприкосновения
                rb.isKinematic = true; // Остановить физику объекта
            }
        }

        if (Input.GetMouseButtonUp(0)) // При отпускании ЛКМ
        {
            rb.isKinematic = false; // Возобновить физику объекта
        }
    }

    void FixedUpdate()
    {
        if (rb.isKinematic)
        {
            rb.MovePosition(grapplePoint); // Перемещаем объект к точке соприкосновения
            rb.centerOfMass = transform.InverseTransformPoint(grapplePoint); // Устанавливаем центр тяжести в точку соприкосновения
        }
    }
}
