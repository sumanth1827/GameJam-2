using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymovement : MonoBehaviour
{
    public float moveSpeed = 1f;     // Speed of movement in blocks per second
    public float maxDistance = 5f;   // Maximum distance to move

    private bool movingRight = true; // Direction flag
    private float distanceMoved = 0f; // Distance moved so far
    
    // Update is called once per frame
    void Update()
    {
        float movement = moveSpeed * Time.deltaTime;

        // Update position based on direction
        if (movingRight)
        {
            transform.Translate(Vector3.right * movement);
        }
        else
        {
            transform.Translate(Vector3.left * movement);
        }

        // Update distance moved
        distanceMoved += movement;

        // Check if reached the maximum distance
        if (distanceMoved >= maxDistance)
        {
            // Reverse direction
            movingRight = !movingRight;
            distanceMoved = 0f;
        }
    }
}
