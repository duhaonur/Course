using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    [SerializeField] private float maxSpeed = 12;
    [SerializeField] private float speedMultiplier = 1;

    private Rigidbody ballRB;

    private Vector3 velocity;
    private Vector3 current;

    private bool goRight, goLeft = false;
    private bool gameRunning = false;

    private void Awake() => ballRB = GetComponent<Rigidbody>();

    private void OnEnable()
    {
        EventManager.OnStartGame += StartGame;
        EventManager.OnStopGame += StopGame;
    }

    private void OnDisable()
    {
        EventManager.OnStartGame -= StartGame;
        EventManager.OnStopGame -= StopGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            goLeft = false;
            goRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            goRight = false;
            goLeft = true;
        }
    }
    private void FixedUpdate()
    {
        if (!gameRunning)
            return;

        if (speed + speedMultiplier < maxSpeed)
            speedMultiplier += Time.deltaTime / 4;

        MoveBall();
    }
    private void MoveBall()
    {

        if (goRight)
        {
            if (current.x > 0.75f)
            {
                goRight = false;
                return;
            }
            velocity = speed * speedMultiplier * Time.deltaTime * (Vector3.right + Vector3.forward).normalized;
        }
        else if (goLeft)
        {
            if (current.x < -0.75f)
            {
                goLeft = false;
                return;
            }
            velocity = speed * speedMultiplier * Time.deltaTime * (Vector3.left + Vector3.forward).normalized;
        }
        else
        {
            velocity = speed * speedMultiplier * Time.deltaTime * Vector3.forward.normalized;
        }
        velocity.y = ballRB.velocity.y * speed * Time.deltaTime;
        current = transform.position + velocity * speed * Time.deltaTime;

        ballRB.MovePosition(transform.position + speed * Time.deltaTime * velocity);

    }
    private void StartGame()
    {
        gameRunning = true;
    }

    private void StopGame()
    {
        gameRunning = false;
    }
}
