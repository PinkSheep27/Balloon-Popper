using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject instructionsPanel;
    public GameObject settingsPanel;
    public GameObject highScorePanel;

    [Header("UI Elements")]
    public Slider volumeSlider;

    [Header("Theme Settings")]
    public SpriteRenderer backgroundToChange;
    public Sprite normalBackground;
    public Sprite darkBackground;

    public static bool isDarkMode = false;

    void Start()
    {
        volumeSlider.value = AudioListener.volume;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenInstructions()
    {
        mainPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMain()
    {
        instructionsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        highScorePanel.SetActive(false);

        mainPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    public void SetTheme(bool isDark)
    {
        isDarkMode = isDark;

        if (isDark)
        {
            backgroundToChange.sprite = darkBackground;
        }
        else
        {
            backgroundToChange.sprite = normalBackground;
        }
    }
    public void OpenHighScores()
    {
        mainPanel.SetActive(false);
        highScorePanel.SetActive(true);
    }
}