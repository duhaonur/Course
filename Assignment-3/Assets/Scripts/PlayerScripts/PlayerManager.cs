using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;

    [SerializeField] private int health;

    private bool gameEnded;

    private void OnEnable() => MyEvents.OnDecreaseHealth += DecreaseHealth;
    private void OnDisable() => MyEvents.OnDecreaseHealth -= DecreaseHealth;

    private void Start()
    {
        health = maxHealth;
        MyEvents.CallUpdateHealthUI(health);
        MyEvents.CallUpdateHealth(health);
    }
    private void DecreaseHealth(int amount)
    {
        health -= amount;
        MyEvents.CallChangeLane(Constants.LANE_COLLISION);
        MyEvents.CallUpdateHealth(health);

        if (health <= 0)
            MyEvents.CallGameEnded(false);
    }
}
