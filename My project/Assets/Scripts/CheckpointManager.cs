using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Vector3 respawnPosition;

    private void Start()
    {
        respawnPosition = transform.position; // Set the initial respawn position to the manager's position
    }

    public void SetRespawnPosition(Vector3 position)
    {
        respawnPosition = position; // Update the respawn position when a checkpoint is reached
    }

    public void RespawnPlayer(Vector3 position)
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>(); // Find the PlayerMovement component in the scene
        if (player != null)
        {
            player.Respawn(position); // Call the Respawn method of the PlayerMovement script and pass the respawn position
        }
    }
}
