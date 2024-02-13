using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float Xpos;
    [SerializeField] private float Ypos;
    [SerializeField] private float Y_Limite;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position = new Vector2(Xpos, Ypos + Mathf.PingPong(Time.time * speed, Y_Limite));
    }
}
