using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float shakeDureation = 0.1f;
    [SerializeField] private float shakeIntensity = 0.05f;

    [SerializeField] private float tntshakeDureation = 0.1f;
    [SerializeField] private float tntshakeIntensity = 0.05f;
    [Space(20)]
    [SerializeField] private Transform cameraTransform;

    private Vector3 originalPosition;

    private void Start()
    {
        Noobik.ShootEvent.AddListener(Shake);
        TNT.TNTExplosion.AddListener(ExplosionShake);

        originalPosition = cameraTransform.position;
    }

    private void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private void ExplosionShake()
    {
        StartCoroutine(ExplosionShakeCoroutine());
    }


    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDureation)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
            cameraTransform.position = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = originalPosition;
    }

    private IEnumerator ExplosionShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < tntshakeDureation)
        {
            Vector3 randomOffset = Random.insideUnitSphere * tntshakeIntensity;
            cameraTransform.position = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = originalPosition;
    }
}