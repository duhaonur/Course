using UnityEngine;

public class LevelCompletedCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            EventManager.CallLevelCompleted();
    }
}
