using UnityEngine;
using UnityEngine.Pool;
public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Car[] cars;

    [SerializeField] private float[] lanes;

    [SerializeField] private float carSpawnStartPoint;

    [SerializeField] private int maxPoolSize;

    private Camera mainCam;

    private ObjectPool<Car> carPool;

    private float farClipPlane;
    private float spawnedCarLength;

    private void Start()
    {
        carPool = new ObjectPool<Car>(SpawnCar, GetCar, ReturnCarToPool, null, false, 10, maxPoolSize);

        mainCam = Camera.main;
        farClipPlane = mainCam.farClipPlane;

        spawnedCarLength = carSpawnStartPoint;
    }

    private void LateUpdate()
    {
        Vector3 camPos = mainCam.transform.TransformPoint(0, 0, farClipPlane);

        if (spawnedCarLength < camPos.z)
        {
            carPool.Get();
        }
    }

    private Car SpawnCar()
    {
        var spawnedCar = Instantiate(cars[Random.Range(0, cars.Length)],transform);

        spawnedCar.gameObject.SetActive(false);
        spawnedCar.GetPool(carPool);

        return spawnedCar;
    }

    private void GetCar(Car car)
    {
        float getNewLane = lanes[Random.Range(0, lanes.Length)];

        Vector3 newPosition = new Vector3(getNewLane, 2f, spawnedCarLength);
        car.transform.position = newPosition;

        car.carReleased = false;

        car.gameObject.SetActive(true);

        spawnedCarLength += car.objLength;
    }

    private void ReturnCarToPool(Car car)
    {
        car.gameObject.SetActive(false);
        car.StopTheCar();
    }
}
