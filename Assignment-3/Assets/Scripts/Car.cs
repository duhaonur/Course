using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    [SerializeField] private float carSpeed;
    [SerializeField] private float startMovingDistance;
    [SerializeField] private float releaseLength;

    private Rigidbody carRB;

    private Camera mainCam;

    private ObjectPool<Car> pool;

    public float objLength;

    public bool carReleased = false;

    private void Start()
    {
        carRB = GetComponent<Rigidbody>();

        mainCam = Camera.main;
    }

    private void Update()
    {
        if (!carReleased && mainCam.transform.position.z > transform.position.z + releaseLength)
        {
            carReleased = true;
            pool.Release(this);
        }
    }

    private void FixedUpdate()
    {
        if (carReleased)
            return;

        carRB.AddForce(Vector3.forward * carSpeed, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.PLAYER))
        {
            MyEvents.CallDecreaseHealth(1);
        }
    }

    public void GetPool(ObjectPool<Car> carPool) => pool = carPool;
    public void StopTheCar() => carRB.velocity = carRB.velocity.normalized * 0;
}
