using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float shakeDureation = 0.1f;
    [SerializeField] private float shakeIntensity = 0.05f;
    [Space(20)]
    [SerializeField] private Transform cameraTransform;

    private Vector3 originalPosition;

    private void Start()
    {
        Noobik.ShootEvent.AddListener(Shake);

        originalPosition = cameraTransform.position;
    }

    private void Shake()
    {
        StartCoroutine(ShakeCoroutine());
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
}