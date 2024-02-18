using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveDistance = 1.0f; // Distance to move up and down
    [SerializeField] private float moveSpeed = 1.0f; // Speed of the movement

    private Vector3 startPos; // Starting position of the object

    void Start()
    {
        startPos = transform.position; // Store the starting position of the object
    }

    void Update()
    {
        float newYPos = startPos.y + Mathf.PingPong(Time.time * moveSpeed, moveDistance); // Calculate the new Y position using Mathf.PingPong function
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z); // Update the object's position
    }
}