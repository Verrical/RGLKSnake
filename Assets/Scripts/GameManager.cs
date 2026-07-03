using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_Text scoreText;     
    public GameObject gameOverPanel; 
    public TMP_Text gameOverScoreText;  
    public TMP_Text gameOverHighScoreText; 
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
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        AudioManager.Instance.OnGameOver();
        if (gameOverScoreText) gameOverScoreText.text = "Score: " + score;
        if (gameOverHighScoreText) gameOverHighScoreText.text = "Best: " + highScore;
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