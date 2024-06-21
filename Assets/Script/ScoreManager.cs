using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;

    [SerializeField] private int playerScoreIncrement = 1;
    [SerializeField] private int enemyScoreIncrement = 1;

    [SerializeField] private int playerScore;
    [SerializeField] private int enemyScore;
    
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<ScoreManager>();
                    singleton.name = typeof(ScoreManager).ToString();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdatePlayerScoreUI();
        UpdateEnemyScoreUI();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdatePlayerScoreUI();
        UpdateEnemyScoreUI();
    }

    public void SetPlayerScoreText(TextMeshProUGUI newPlayerScoreText)
    {
        playerScoreText = newPlayerScoreText;
        UpdatePlayerScoreUI();
    }

    public void SetEnemyScoreText(TextMeshProUGUI newEnemyScoreText)
    {
        enemyScoreText = newEnemyScoreText;
        UpdateEnemyScoreUI();
    }

    public void AddPlayerScore()
    {
        playerScore += playerScoreIncrement;
        UpdatePlayerScoreUI();
        SaveScores();
    }

    public void AddEnemyScore()
    {
        enemyScore += enemyScoreIncrement;
        UpdateEnemyScoreUI();
        SaveScores();
    }

    private void UpdatePlayerScoreUI()
    {
        if (playerScoreText != null)
        {
            playerScoreText.text = playerScore.ToString();
        }
    }

    private void UpdateEnemyScoreUI()
    {
        if (enemyScoreText != null)
        {
            enemyScoreText.text = enemyScore.ToString();
        }
    }

    private void SaveScores()
    {
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetInt("EnemyScore", enemyScore);
    }

    private void LoadScores()
    {
        if (PlayerPrefs.HasKey("PlayerScore"))
        {
            playerScore = PlayerPrefs.GetInt("PlayerScore");
        }
        else
        {
            playerScore = 0;
        }

        if (PlayerPrefs.HasKey("EnemyScore"))
        {
            enemyScore = PlayerPrefs.GetInt("EnemyScore");
        }
        else
        {
            enemyScore = 0;
        }
    }

    public void ResetScores()
    {
        playerScore = 0;
        enemyScore = 0;
        UpdatePlayerScoreUI();
        UpdateEnemyScoreUI();
        SaveScores();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}