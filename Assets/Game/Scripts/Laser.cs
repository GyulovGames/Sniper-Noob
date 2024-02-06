using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform laserTransform;
    [SerializeField] private Transform hitPointer;



    public float maxLaserDistance;



    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserTransform.position, laserTransform.right, maxLaserDistance);

        if (hit.collider != null)
        {
            float distance = Vector2.Distance(laserTransform.position, hit.point);
            laserTransform.localScale = new Vector2(distance, laserTransform.localScale.y);
            hitPointer.position = hit.point;
        }
        else
        {
            laserTransform.localScale = new Vector2(maxLaserDistance, laserTransform.localScale.y);
            hitPointer.localPosition = new Vector2(maxLaserDistance, laserTransform.localPosition.y);
        }
    }
}