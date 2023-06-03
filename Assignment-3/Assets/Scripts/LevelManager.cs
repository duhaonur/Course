using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;

    [SerializeField] private Slider slider;

    [SerializeField] private Transform player;

    [SerializeField] private float[] levelLength;

    private int currentLevel;

    private bool gameEnded;

    private void OnEnable()
    {
        MyEvents.OnGameEnded += GameEnded;
    }
    private void OnDisable()
    {
        MyEvents.OnGameEnded -= GameEnded;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(Constants.FIRST_SAVE) != 1)
        {
            PlayerPrefs.SetInt(Constants.FIRST_SAVE, 1);
            PlayerPrefs.SetInt(Constants.LEVEL, 1);
        }
        else
        {
            if (PlayerPrefs.GetInt(Constants.LEVEL) <= levelLength.Length)
                currentLevel = PlayerPrefs.GetInt(Constants.LEVEL) - 1;
            else
                currentLevel = levelLength.Length - 1;
        }

        currentLevelText.text = PlayerPrefs.GetInt(Constants.LEVEL).ToString();
        nextLevelText.text = (PlayerPrefs.GetInt(Constants.LEVEL) + 1).ToString();
    }

    private void Update()
    {
        if (gameEnded)
            return;

        if (player.transform.position.z > levelLength[currentLevel])
        {
            MyEvents.CallGameEnded(true);
        }
        slider.value = Mathf.InverseLerp(0, levelLength[currentLevel], player.transform.position.z);
    }

    private void GameEnded(bool obj)
    {
        gameEnded = true;
    }
}
