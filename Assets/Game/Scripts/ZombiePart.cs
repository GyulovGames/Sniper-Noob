using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePart : MonoBehaviour
{
    [SerializeField] private Zombie zombie;
    [SerializeField] private HingeJoint2D hingeJoint2D;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.tag == "Bullet")
        {
            zombie.Hit();
        }
        else if(collision.relativeVelocity.magnitude >4)
        {
            zombie.Hit();
        }
    }
}