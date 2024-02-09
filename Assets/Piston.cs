using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform target; // Целевая позиция
    public float speed = 5f; // Скорость движения

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        // Вычисляем направление к целевой позиции
        Vector2 direction = (target.position - transform.position).normalized;

        // Вычисляем новую позицию с учетом скорости движения
        Vector2 newPosition = (Vector2)transform.position + (direction * speed * Time.fixedDeltaTime);

        // Перемещаем дочерний объект в новую позицию
        rb.MovePosition(newPosition);
    }
}
