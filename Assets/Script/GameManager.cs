using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void BallCaptured()
    {
        Time.timeScale = 0f;
        ScoreManager.Instance.AddEnemyScore();
        canvas.gameObject.SetActive(true);
    }

    public void Goal()
    {
        Time.timeScale = 0f;
        ScoreManager.Instance.AddPlayerScore();
        canvas.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}