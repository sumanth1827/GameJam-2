using UnityEngine;
using UnityEngine.SceneManagement;

public class deathmenu : MonoBehaviour
{
    public GameObject death;
    public string mainMenuSceneName;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
