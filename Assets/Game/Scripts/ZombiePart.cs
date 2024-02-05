using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePart : MonoBehaviour
{
    [SerializeField] private  Zombie zombie;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Bullet")
        {
            zombie.Hit();
        }
    }
}