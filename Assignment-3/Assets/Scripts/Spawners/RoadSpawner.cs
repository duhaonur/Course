using UnityEngine;
using UnityEngine.Pool;
public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private Road road;

    [SerializeField] private float roadLength;

    [SerializeField] private int maxPoolSize;

    private Camera mainCam;

    private ObjectPool<Road> roadPool;

    private float farClipPlane;
    private float spawnedRoadLength;

    private void Awake()
    {
        roadPool = new ObjectPool<Road>(SpawnRoad, GetRoad, ReturnRoadToPool, null, false, 10, maxPoolSize);

        mainCam = Camera.main;
        farClipPlane = mainCam.farClipPlane;

        spawnedRoadLength = 0;
    }

    private void Update()
    {
        Vector3 camPos = mainCam.transform.TransformPoint(0, 0, farClipPlane);

        if (spawnedRoadLength - roadLength <= camPos.z)
        {
            roadPool.Get();
        }
    }

    private Road SpawnRoad()
    {
        var spawnedRoad = Instantiate(road, transform);

        spawnedRoad.gameObject.SetActive(false);
        spawnedRoad.GetPool(roadPool);

        return spawnedRoad;
    }

    private void GetRoad(Road road)
    {
        Vector3 newPosition = new Vector3(0, 0, spawnedRoadLength);
        road.transform.position = newPosition;

        road.SetNewPosition(newPosition);

        road.roadReleased = false;

        road.gameObject.SetActive(true);

        spawnedRoadLength += road.objLength;
    }

    private void ReturnRoadToPool(Road road) => road.gameObject.SetActive(false);
}
