using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    [SerializeField] private float tntSensitive;
    [SerializeField] private float explosionRadius;
    [Space(15)]
    [SerializeField] private AudioClip tntIsFireSound;
    [SerializeField] private AudioClip tntExplosionSound;
    [Space(10)]
    [SerializeField] private Rigidbody2D rigidbody2;
    [SerializeField] private Animator tntAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private ParticleSystem explosionParticles;

    private bool isCollided = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isCollided && collision.relativeVelocity.magnitude > tntSensitive)
        {
            isCollided = true;
            Fire();
        }
    }

    public void Fire()
    {
        isCollided = true;
        tntAnimator.enabled = true;
        audioSource.clip = tntIsFireSound;
        audioSource.Play();
    }

    private void Explosion()
    {
        tntAnimator.enabled = false;
        rigidbody2.simulated = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;

        audioSource.clip = tntExplosionSound;
        audioSource.Play();
        explosionParticles.Play();
        Invoke("EnableObject", 1f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach(Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

            if (collider.GetComponent<TNT>())
            {
                TNT tnt = collider.GetComponent<TNT>();
                tnt.Fire();
            }

            if(rb != null)
            {
                Vector2 direction = rb.transform.position - transform.position;
                rb.AddForce(direction.normalized * explosionForce, ForceMode2D.Impulse);
            }
        }
    }

    private void EnableObject()
    {
        audioSource.enabled = false;
        explosionParticles.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}