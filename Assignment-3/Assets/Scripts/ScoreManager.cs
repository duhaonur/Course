using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private float scoreHolder;

    private void OnEnable()
    {
        MyEvents.OnIncreaseScore += IncreaseScore;
    }
    private void OnDisable()
    {
        MyEvents.OnIncreaseScore -= IncreaseScore;
    }
    private void Start()
    {
        scoreText.text = scoreHolder.ToString();
    }
    private void IncreaseScore(float score)
    {
        scoreHolder += score;
        scoreText.text = scoreHolder.ToString();
        MyEvents.CallGetScore(scoreHolder);
    }
}
