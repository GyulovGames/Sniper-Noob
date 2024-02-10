using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int reboundCound;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private TrailRenderer bulletTrail; 
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private ParticleSystem reboundParticles;
    [SerializeField] private AudioSource audioSource;


    private void OnEnable()
    {
        Invoke("Disable", 1f);
    }


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

            case "Rebound":
                reboundCound--;
                audioSource.Play();
                reboundParticles.Play();

                if(reboundCound == 0)
                {
                    transform.parent = collision.gameObject.transform;
                    spriteRenderer.enabled = false;
                    circleCollider.enabled = false;
                    rigidBody2D.simulated = false;
                    bulletTrail.enabled = false;

                    Invoke("Disable", 1f);
                }
                break;

            default:
                transform.parent = collision.gameObject.transform;
                spriteRenderer.enabled = false;
                circleCollider.enabled = false;
                rigidBody2D.simulated = false;
                bulletTrail.enabled = false;
                audioSource.Play();
                reboundParticles.Play();
                Invoke("Disable", 1);
                break;
        }
    }


    private void Disable()
    {
        gameObject.SetActive(false);
    }
}