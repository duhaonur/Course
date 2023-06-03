using UnityEngine;
using UnityEngine.Pool;

public class Building : MonoBehaviour
{
    [SerializeField] private float releaseLength;

    private Camera mainCam;

    private ObjectPool<Building> pool;

    private float zPosition;

    public float objectLength;

    public bool buildingReleased = false;

    private void Start() => mainCam = Camera.main;

    private void Update()
    {
        if (!buildingReleased && mainCam.transform.position.z > zPosition)
        {
            buildingReleased = true;
            pool.Release(this);
        }
    }

    public void GetPool(ObjectPool<Building> buildingPool) => pool = buildingPool;
    public void SetNewPosition(Vector3 newPosition) => zPosition = newPosition.z + releaseLength;
}
