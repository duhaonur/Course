using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Transform imageParent;

    [SerializeField] private Image healthImagePrefab;

    [SerializeField] private float healthImageWidth;
    [SerializeField] private float perImageSpace;

    private List<Image> healthImage;

    private void OnEnable()
    {
        MyEvents.OnUpdateHealth += UpdateHealth;
        MyEvents.OnUpdateHealthUI += UpdateHealthUI;
    }

    private void OnDisable()
    {
        MyEvents.OnUpdateHealth -= UpdateHealth;
        MyEvents.OnUpdateHealthUI -= UpdateHealthUI;
    }

    private void UpdateHealth(int health)
    {
        for (int i = 3; i > health; i--)
        {
            healthImage[i - 1].enabled = false;
        }
    }
    private void UpdateHealthUI(int healthAmount)
    {
        healthImage = new List<Image>();

        for (int i = 0; i < healthAmount; i++)
        {
            var spawnedImg = Instantiate(healthImagePrefab, imageParent);

            var spawnedImggRectTransform = spawnedImg.GetComponent<RectTransform>();
            spawnedImggRectTransform.localPosition = new Vector3(healthImageWidth * i + perImageSpace, 0, 0);

            healthImage.Add(spawnedImg);
        }
    }


}
