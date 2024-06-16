using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI goalText;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        canvas.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

    public void Goal()
    {
        Time.timeScale = 0f;
        canvas.gameObject.SetActive(true);
        goalText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
