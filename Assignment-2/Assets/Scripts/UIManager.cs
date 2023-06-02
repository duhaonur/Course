using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endLevelText;
    [SerializeField] private TextMeshProUGUI playAgainText;

    [SerializeField] private Color levelFailedColor;
    [SerializeField] private Color levelCompletedColor;

    private void Start() => playAgainText.text = ">> PRESS 'SPACE' TO PLAY AGAIN <<";
    private void OnEnable()
    {
        EventManager.OnLevelCompleted += LevelCompleted;
        EventManager.OnLevelFailed += LevelFailed;
    }
    private void OnDisable()
    {
        EventManager.OnLevelCompleted -= LevelCompleted;
        EventManager.OnLevelFailed -= LevelFailed;
    }
    private void LevelFailed()
    {
        endLevelText.text = "Level Failed";
        endLevelText.color = levelFailedColor;
        playAgainText.gameObject.SetActive(true);
        endLevelText.gameObject.SetActive(true);
        EventManager.CallStopGame();
    }

    private void LevelCompleted()
    {
        endLevelText.text = "Level Completed";
        endLevelText.color = levelCompletedColor;
        playAgainText.gameObject.SetActive(true);
        endLevelText.gameObject.SetActive(true);
        EventManager.CallStopGame();
    }
}
