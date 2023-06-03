using UnityEngine;
using UnityEngine.Pool;
[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour
{
    [SerializeField] private float releaseLength;
    [SerializeField] private float scoreIncreaseAmount;

    private Rigidbody coinRB;

    private Camera mainCam;

    private ObjectPool<Coin> pool;

    public float objLength;

    public bool coinReleased = false;

    private void Start()
    {
        coinRB = GetComponent<Rigidbody>();
        coinRB.useGravity = false;
        coinRB.isKinematic = true;

        mainCam = Camera.main;
    }

    private void Update()
    {
        if (!coinReleased && mainCam.transform.position.z > transform.position.z + releaseLength)
        {
            coinReleased = true;
            pool.Release(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PLAYER))
        {
            MyEvents.CallIncreaseScore(scoreIncreaseAmount);
            coinReleased = true;
            pool.Release(this);
        }
    }

    public void GetPool(ObjectPool<Coin> coinPool) => pool = coinPool;
}
