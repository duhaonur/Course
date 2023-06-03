using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelCompletedFailedText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI remainingHeathScoreMultiplierText;
    [SerializeField] private TextMeshProUGUI endGameButtonText;

    [SerializeField] private Button endGameButton;

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float timeToDisplayCanvas;
    [SerializeField] private float maxCanvasAlphaAmount;
    [SerializeField] private float remainingHealthMultiplier;

    private float timer;
    private float score;

    private int health;

    private bool gameEnded;
    private bool levelCompleted;

    private void OnEnable()
    {
        MyEvents.OnGetScore += GetScore;
        MyEvents.OnUpdateHealth += GetRemainingHealth;
        MyEvents.OnGameEnded += GameEnded;
    }
    private void OnDisable()
    {
        MyEvents.OnGetScore -= GetScore;
        MyEvents.OnUpdateHealth -= GetRemainingHealth;
        MyEvents.OnGameEnded -= GameEnded;
    }
    private void Start() => gameEnded = false;

    private void Update()
    {
        if (!gameEnded || canvasGroup.alpha >= 0.5f)
            return;

        timer += Time.deltaTime;

        canvasGroup.alpha = Mathf.InverseLerp(canvasGroup.alpha, timeToDisplayCanvas, timer);
    }

    public void NextLevelPlayAgainButton()
    {
        if (levelCompleted)
        {
            int currentLevel = PlayerPrefs.GetInt(Constants.LEVEL);
            PlayerPrefs.SetInt(Constants.LEVEL, currentLevel + 1);
            SceneManager.LoadScene(Constants.PLAY_SCENE);
        }
        else
        {
            SceneManager.LoadScene(Constants.PLAY_SCENE);
        }
    }
    private void GetScore(float score) => this.score = score;
    private void GetRemainingHealth(int health) => this.health = health;

    private void GameEnded(bool status)
    {
        gameEnded = true;
        levelCompleted = status;

        if (status)
        {
            levelCompletedFailedText.text = "Level Completed";
            endGameButtonText.text = "Next Level";

            remainingHeathScoreMultiplierText.text = health + " X " + remainingHealthMultiplier;

            score = score == 0 ? 1 : score;
            float totalScore = health * remainingHealthMultiplier * score;

            totalScoreText.text = totalScore.ToString();

            remainingHeathScoreMultiplierText.enabled = true;
            totalScoreText.enabled = true;
        }
        else
        {
            levelCompletedFailedText.text = "Level Failed";
            endGameButtonText.text = "Play Again";

            remainingHeathScoreMultiplierText.text = health + " X " + remainingHealthMultiplier;

            float totalScore = health * remainingHealthMultiplier * score;

            totalScoreText.text = totalScore.ToString(); ;

            endGameButton.gameObject.SetActive(true);
        }
    }
}
