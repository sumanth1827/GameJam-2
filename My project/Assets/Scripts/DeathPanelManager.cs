using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanelManager : MonoBehaviour
{
    public GameObject deathPanel;

    public void ShowDeathPanel()
    {
        Time.timeScale = 0; // Pause the game
        deathPanel.SetActive(true);
    }

    public void RestartGame()
{
    Time.timeScale = 1; // Resume the game
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    
    PlayerMovement player = FindObjectOfType<PlayerMovement>(); // Find the PlayerMovement component in the scene
    if (player != null)
    {
        player.Respawn(); // Call the Respawn method of the PlayerMovement script
    }
}
}