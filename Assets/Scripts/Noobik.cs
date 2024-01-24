using UnityEngine;
using UnityEngine.UIElements;

public class Noobik : MonoBehaviour
{
    [SerializeField] private float neckMaxAngle;
    [SerializeField] private float sholderMaxAngle;
    [SerializeField] private float spineMaxAngle;
    [Space(25)]
    [SerializeField] private Transform neckTransform;
    [SerializeField] private Transform sholderTransform;
    [SerializeField] private Transform spineTransform;
    [SerializeField] private Transform sleeveSpawnerTransform;
    [SerializeField] private Transform rifleBarrelTransform;
    [Space(25)]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject sleeve;
    [SerializeField] private GameObject bullet;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.GetComponent<Camera>();
    }

    private void Update()
    {      
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("One Shoot", true);

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            NeckRotation(mousePosition);
            SholderRotation(mousePosition);
            SpineRotation(mousePosition);
        }
        else if(Input.GetMouseButtonUp(0))
        {
           GameObject bulletObject =  Instantiate(bullet, rifleBarrelTransform.position, rifleBarrelTransform.rotation);
           Rigidbody2D bulletRigid = bulletObject.GetComponent<Rigidbody2D>();
           // bulletRigid.AddForce(rifleBarrelTransform.right * 15, ForceMode2D.Impulse);
            bulletRigid.velocity = rifleBarrelTransform.right * 15f;

            audioSource.Play();
            animator.SetBool("One Shoot", false);
        }
    }

    public void SpawnSleeve()
    {
       Instantiate(sleeve, sleeveSpawnerTransform.position, sleeveSpawnerTransform.rotation);       
    }

    private void NeckRotation(Vector3 mousePos)
    {            
        Vector3 objectPos = mainCamera.WorldToScreenPoint(neckTransform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float rawAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(rawAngle, -neckMaxAngle, neckMaxAngle);
        neckTransform.rotation = Quaternion.Euler(new Vector3(0, 0, clampedAngle));
    }

    private void SholderRotation(Vector3 mousePos)
    {
        Vector3 objectPos = mainCamera.WorldToScreenPoint(neckTransform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float rawAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(rawAngle, -20, sholderMaxAngle);
        sholderTransform.rotation = Quaternion.Euler(new Vector3(0, 0, clampedAngle));
    }

    private void SpineRotation(Vector3 mousePos)
    {
        Vector3 objectPos = mainCamera.WorldToScreenPoint(neckTransform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float rawAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(rawAngle, -spineMaxAngle, spineMaxAngle);
        spineTransform.rotation = Quaternion.Euler(new Vector3(0, 0, clampedAngle));
    }
}