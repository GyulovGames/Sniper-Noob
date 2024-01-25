using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private Transform laserTransform;
    [SerializeField] private Transform hitPointer;


    
    public float maxLaserDistance = 10f;



    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserTransform.position, laserTransform.right, maxLaserDistance);

        if (hit.collider != null)
        {
            float distance = Vector2.Distance(laserTransform.position, hit.point);
            laserTransform.localScale = new Vector2(distance, laserTransform.localScale.y);
            hitPointer.position = hit.point;
        }
    }
}