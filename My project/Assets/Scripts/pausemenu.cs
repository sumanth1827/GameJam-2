using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{
    
   public static bool GameIsPaused =false;
   public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
        
    }
    public void resume()//closes the pause menu and resumes the game
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale=1f;
        GameIsPaused=false;


    }
    void pause()//stops the time for the game and opens pause menu
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale=0f;
        GameIsPaused=true;

    }
 public void LoadMenu()//changes scene to Main menu scene
 {
    SceneManager.LoadScene("Main menu");

 }
 public void QuitGame()//to quit the game
 {
    Application.Quit();
    
 }}