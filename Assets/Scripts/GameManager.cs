using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public Text scoreText;     
    public GameObject gameOverPanel; 

    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (gameOverPanel) gameOverPanel.SetActive(false);
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
    void UpdateScoreUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}