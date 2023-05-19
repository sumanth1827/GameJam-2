using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenumanager : MonoBehaviour
{
    
    public void playgame()//It loads the next scene in the build index
    {  Time.timeScale=1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void exitgame()//It is a C# function that is called when a button in the game is clicked. It quits the game by calling Application.Quit()
    {
        Application.Quit();
    }
}
