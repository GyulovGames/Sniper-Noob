using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class Togle : MonoBehaviour
{
    public UnityEvent action;

    private bool isOpen = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform childTransform = transform.GetChild(0).transform;
        action.Invoke();

        if (!isOpen)
        {
            childTransform.localEulerAngles = new Vector3(0, 0, 30);
            isOpen = true;
        }
        else if (isOpen)
        {
            childTransform.localEulerAngles = new Vector3(0, 0, -30);
            isOpen = false;
        }
    }
}