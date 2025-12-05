using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject pauseMenuUI;

    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int balloonsInLevel;
    private bool isPaused = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject textObj = GameObject.FindGameObjectWithTag("ScoreText");

        if (textObj != null)
        {
            scoreText = textObj.GetComponent<TextMeshProUGUI>();
            UpdateScoreText();
        }
        //scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        //UpdateScoreText();

        balloonsInLevel = GameObject.FindGameObjectsWithTag("Balloon").Length;

        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        if (canvas != null)
        {
            Transform pauseMenuTransform = canvas.transform.Find("PauseMenu");
            if (pauseMenuTransform != null)
            {
                pauseMenuUI = pauseMenuTransform.gameObject;
                pauseMenuUI.SetActive(isPaused);
            }
            else
            {
                Debug.LogWarning("GameManager: Could not find 'PauseMenu' object.");
            }
        }
        else
        {
            Debug.LogWarning("GameManager: Could not find 'MainCanvas' object.");
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned in GameManager.");
        }
    }
    public void BalloonPopped()
    {
        balloonsInLevel--;

        if (balloonsInLevel <= 0)
        {
            LoadNextLevel();
        }
    }

    public void RestartLevel()
    {
        if (isPaused) TogglePause();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        if (isPaused) TogglePause();
      
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            HighScoreManager.SaveScore(score);

            score = 0;

            SceneManager.LoadScene(0);
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if(pauseMenuUI != null) pauseMenuUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if(pauseMenuUI != null) pauseMenuUI.SetActive(false);
        }
    }
}