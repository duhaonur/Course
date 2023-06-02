using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Countdown : MonoBehaviour
{
    [SerializeField]private float countdown = 3;

    private TextMeshProUGUI countdownText;

    private void Start() => countdownText = GetComponent<TextMeshProUGUI>();

    private void Update()
    {
        if (countdown > 1)
        {
            countdown -= Time.deltaTime;
            countdownText.text = countdown.ToString("N0");
        }
        else
        {
            countdownText.gameObject.SetActive(false);
            EventManager.CallStartGame();
            gameObject.SetActive(false);
        }
    }

}
