using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private float scoreHolder = 0;

    private bool gameRuning = false;

    private void Start() => scoreText = GetComponent<TextMeshProUGUI>();

    private void OnEnable()
    {
        EventManager.OnStartGame += StartGame;
        EventManager.OnStopGame += StopGame;
        EventManager.OnScoreIncreased += IncreaseScore;
    }


    private void OnDisable()
    {
        EventManager.OnStartGame -= StartGame;
        EventManager.OnStopGame -= StopGame;
        EventManager.OnScoreIncreased -= IncreaseScore;
    }
    private void Update()
    {
        if (gameRuning)
        {
            scoreHolder += Time.deltaTime / 2;
            scoreText.text = "SCORE: " + scoreHolder.ToString("N0"); 
        }
    }
    private void IncreaseScore()
    {
        scoreHolder += 10;
    }

    private void StartGame()
    {
        gameRuning = true;
    }

    private void StopGame()
    {
        gameRuning = false;
    }
}
