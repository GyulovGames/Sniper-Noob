using UnityEngine;

public class ToggleHandle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Togle togle = transform.parent.GetComponent<Togle>();
        togle.ToggleSwitch();
    }
}
