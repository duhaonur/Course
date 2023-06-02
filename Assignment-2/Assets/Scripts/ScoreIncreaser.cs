using UnityEngine;

public class ScoreIncreaser : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            EventManager.CallIncreaseScore();
    }
}
