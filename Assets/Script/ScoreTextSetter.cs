using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextSetter : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;
    public Button resetButton;

    private void Start()
    {
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            if (playerScoreText != null)
            {
                scoreManager.SetPlayerScoreText(playerScoreText);
            }
            if (enemyScoreText != null)
            {
                scoreManager.SetEnemyScoreText(enemyScoreText);
            }
            if (resetButton != null)
            {
                resetButton.onClick.RemoveAllListeners();
                resetButton.onClick.AddListener(scoreManager.ResetScores);
            }
        }
        else
        {
            Debug.LogWarning("ScoreManager not found.");
        }
    }
}
