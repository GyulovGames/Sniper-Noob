using UnityEngine;
using UnityEngine.Events;

public class Togle : MonoBehaviour
{
    public UnityEvent action;

    private bool isOpen = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ToggleSwitch();
    }

    public void ToggleSwitch()
    {
        action.Invoke();
        Transform childTransform = transform.GetChild(0).transform;

        if (!isOpen)
        {
            childTransform.localEulerAngles = new Vector3(0, 0, 30);
            isOpen = true;
        }
        else
        {
            childTransform.localEulerAngles = new Vector3(0, 0, -30);
            isOpen = false;
        }
    }
}