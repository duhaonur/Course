using System.Collections;
using System.Threading;
using UnityEngine;
using Cinemachine;

public class PlayerCarMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;

    [SerializeField, Header("0 = Left, 1 = Middle, 2 = Right")] private float[] lanes;

    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float timeToMoveNewLane;

    private PlayerSwipeDetection swipeDetection;

    private CancellationTokenSource tokenSource;

    private Coroutine previousCoroutine;

    private Collider playerCollider;

    private Vector3 direction;

    private int currentLane;

    private bool gameEnded;

    private void Awake()
    {
        tokenSource = new CancellationTokenSource();

        swipeDetection = PlayerSwipeDetection.Instance;
    }
    private void OnEnable()
    {
        swipeDetection.OnTouchPerformed += GetDesiredLane;
        MyEvents.OnChangeLane += GetDesiredLane;
        MyEvents.OnGameEnded += GameEnded;
    }
    private void OnDisable()
    {
        swipeDetection.OnTouchPerformed -= GetDesiredLane;
        MyEvents.OnChangeLane -= GetDesiredLane;
        MyEvents.OnGameEnded -= GameEnded;
        tokenSource.Cancel();
    }
    private void Start()
    {
        playerCollider = GetComponent<Collider>();

        currentLane = 1;
        previousCoroutine = StartCoroutine(ChangeLane(currentLane));
    }
    private void Update()
    {
        if (gameEnded)
            return;

        MoveCar();
    }
    private void MoveCar()
    {
        speed += Time.deltaTime * speedMultiplier;
        transform.Translate(speed * Time.deltaTime * Vector3.forward, Space.World);
    }
    public void GetDesiredLane(string lane)
    {
        if (lane == Constants.INVALID_SWIPE)
            return;

        if(lane == Constants.LANE_COLLISION)
        {
            if (currentLane != 0)
                currentLane--;
            else
                currentLane++;
        }
        else if (lane == Constants.LANE_LEFT)
        {
            if (currentLane != 0)
                currentLane--;
        }
        else if (lane == Constants.LANE_RIGHT)
        {
            if (currentLane != 2)
                currentLane++;
        }
        if (previousCoroutine != null)
            StopCoroutine(previousCoroutine);

        previousCoroutine = StartCoroutine(ChangeLane(currentLane));
    }

    private IEnumerator ChangeLane(int lane)
    {
        float timer = 0;

        while (timer < timeToMoveNewLane && !tokenSource.IsCancellationRequested)
        {
            direction = transform.position;
            direction.x = lanes[lane];

            transform.position = Vector3.Lerp(transform.position, direction, timer / timeToMoveNewLane);

            timer += Time.deltaTime;

            yield return null;
        }

        transform.position = direction;
    }
    private void GameEnded(bool status)
    {
        vCam.Follow = null;
        vCam.LookAt = null;
        playerCollider.enabled = false;
    }
}
