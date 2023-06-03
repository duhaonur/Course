using UnityEngine;
using UnityEngine.Pool;
public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private Building[] buildings;

    [SerializeField] private float xSpawnPos;

    [SerializeField] private int maxPoolSize;

    private Camera mainCam;

    private ObjectPool<Building> buildingPool;

    private float farClipPlane;
    private float spawnedBuildingsLength;
    private float previousBuildingLength;

    private void Start()
    {
        buildingPool = new ObjectPool<Building>(SpawnRandomBuilding, GetBuilding, ReturnBuildingToPool, null, false, 10, maxPoolSize);

        mainCam = Camera.main;
        farClipPlane = mainCam.farClipPlane;

        spawnedBuildingsLength = 0;
        previousBuildingLength = 0;
    }

    private void Update()
    {
        Vector3 camPos = mainCam.transform.TransformPoint(0, 0, farClipPlane);

        if (spawnedBuildingsLength < camPos.z)
        {
            buildingPool.Get();
        }
    }

    private Building SpawnRandomBuilding()
    {
        var building = Instantiate(buildings[Random.Range(0, buildings.Length)], transform);

        building.gameObject.SetActive(false);
        building.GetPool(buildingPool);

        return building;
    }

    private void GetBuilding(Building building)
    {
        spawnedBuildingsLength += previousBuildingLength;
        previousBuildingLength = building.objectLength;

        Vector3 newPosition = new Vector3(xSpawnPos, 0, spawnedBuildingsLength);
        building.transform.position = newPosition;

        building.SetNewPosition(newPosition);

        building.buildingReleased = false;
        building.gameObject.SetActive(true);

    }

    private void ReturnBuildingToPool(Building building) => building.gameObject.SetActive(false);
}
