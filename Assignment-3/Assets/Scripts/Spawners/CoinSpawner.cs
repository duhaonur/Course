using UnityEngine;
using UnityEngine.Pool;
public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin coin;

    [SerializeField] private float[] lanes;

    [SerializeField, Range(0f, 1f)] private float coinSpawnChance;
    [SerializeField] private float coinSpawnStartLength;
    [SerializeField] private float distanceBetweenNextCoinSpawn;
    [SerializeField] private float coinLength;

    [SerializeField] private int maxPoolSize;
    [SerializeField] private int minCoinSpawn;
    [SerializeField] private int maxCoinSpawn;

    private Camera mainCam;

    private ObjectPool<Coin> coinPool;

    private float farClipPlane;
    private float spawnedCoinLength;
    private float newLane;

    private int randomCoinAmount;

    private void Start()
    {
        coinPool = new ObjectPool<Coin>(SpawnCoin, GetCoin, ReturnCoinToPool, null, false, 10, maxPoolSize);

        mainCam = Camera.main;
        farClipPlane = mainCam.farClipPlane;
        spawnedCoinLength = coinSpawnStartLength;
    }

    private void Update()
    {
        Vector3 camPos = mainCam.transform.TransformPoint(0, 0, farClipPlane);

        if (spawnedCoinLength < camPos.z)
        {
            randomCoinAmount = Random.Range(minCoinSpawn, maxCoinSpawn);
            newLane = lanes[Random.Range(0, lanes.Length)];

            if (Random.Range(0, 1) < coinSpawnChance)
            {
                for (int i = 0; i < randomCoinAmount; i++)
                {
                    coinPool.Get();
                    spawnedCoinLength += coinLength;
                }
                spawnedCoinLength += distanceBetweenNextCoinSpawn;
            }
            else
            {
                spawnedCoinLength += coinLength * randomCoinAmount + distanceBetweenNextCoinSpawn;
            }
        }
    }

    private Coin SpawnCoin()
    {
        var spawnedCar = Instantiate(coin, transform);

        spawnedCar.gameObject.SetActive(false);
        spawnedCar.GetPool(coinPool);

        return spawnedCar;
    }

    private void GetCoin(Coin coin)
    {
        Vector3 newPosition = new Vector3(newLane, coin.transform.position.y, spawnedCoinLength);
        coin.transform.position = newPosition;

        coin.coinReleased = false;

        coin.gameObject.SetActive(true);
    }

    private void ReturnCoinToPool(Coin coin) => coin.gameObject.SetActive(false);
}
