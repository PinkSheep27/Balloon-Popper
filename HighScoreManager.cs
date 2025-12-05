using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour
{
    public TMP_Text[] entryTextObjects;

    void Start()
    {
        UpdateUI();
    }

    public static void SaveScore(int newScore)
    {
        List<int> highScores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("HighScore" + i, 0));
        }

        highScores.Add(newScore);

        highScores.Sort((a, b) => b.CompareTo(a));

        if (highScores.Count > 5)
        {
            highScores.RemoveAt(5);
        }

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i]);
        }
        PlayerPrefs.Save();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < 5; i++)
        {
            int score = PlayerPrefs.GetInt("HighScore" + i, 0);

            if (i < entryTextObjects.Length)
            {
                entryTextObjects[i].text = (i + 1) + ". " + score;
            }
        }
    }

    [ContextMenu("Reset High Scores")]
    public void ResetHighScores()
    {
        PlayerPrefs.DeleteAll();
        UpdateUI();
    }
}