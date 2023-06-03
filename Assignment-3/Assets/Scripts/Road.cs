using UnityEngine;
using UnityEngine.Pool;

public class Road : MonoBehaviour
{
    [SerializeField] private float releaseLength;

    private Camera mainCam;

    private ObjectPool<Road> pool;

    private float zPosition;

    public float objLength;

    public bool roadReleased = false;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (!roadReleased && mainCam.transform.position.z > zPosition)
        {
            roadReleased = true;
            pool.Release(this);
        }
    }

    public void GetPool(ObjectPool<Road> roadPool) => pool = roadPool;
    public void SetNewPosition(Vector3 newPosition) => zPosition = newPosition.z + releaseLength;
}
