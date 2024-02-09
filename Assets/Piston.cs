using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform target; // ������� �������
    public float speed = 5f; // �������� ��������

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        // ��������� ����������� � ������� �������
        Vector2 direction = (target.position - transform.position).normalized;

        // ��������� ����� ������� � ������ �������� ��������
        Vector2 newPosition = (Vector2)transform.position + (direction * speed * Time.fixedDeltaTime);

        // ���������� �������� ������ � ����� �������
        rb.MovePosition(newPosition);
    }
}
