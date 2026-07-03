using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Replace "GameScene" with whatever your game scene is named
    public void StartGame()
    {
        SceneManager.LoadScene("SnakeGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}