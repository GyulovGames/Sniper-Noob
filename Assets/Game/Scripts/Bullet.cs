using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private TrailRenderer bulletTrail; 
    [SerializeField] private ParticleSystem hitParticles;

    public Vector2 initialDirection;
    public int reflectionCount = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "ZombiePart":
                transform.parent = collision.gameObject.transform;
                spriteRenderer.enabled = false;
                circleCollider.enabled = false;
                rigidBody2D.simulated = false;
                bulletTrail.enabled = false;
                hitParticles.Play();
                break;
        }
    }
}