using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerSwipeDetection : Singleton<PlayerSwipeDetection>
{
    #region Events
    public delegate void TouchPerformed(string direction);
    public event TouchPerformed OnTouchPerformed;
    #endregion

    [SerializeField, Range(0f, 1f)] private float minDist;
    [SerializeField, Range(0f, 1f)] private float directionThreshold;
    [SerializeField] private float maxTime;

    private PlayerInputManager playerInput;

    private Vector2 startPosition;
    private Vector2 endPosition;

    private float startTime;
    private float endTime;

    private void Awake() => playerInput = PlayerInputManager.Instance;

    private void OnEnable()
    {
        playerInput.OnStartTouch += SwipeStart;
        playerInput.OnEndTouch += SwipeEnd;
    }
    private void OnDisable()
    {
        playerInput.OnStartTouch -= SwipeStart;
        playerInput.OnEndTouch -= SwipeEnd;
    }
    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }
    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition.normalized, endPosition.normalized) >= minDist && (endTime - startTime) <= maxTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        string directionName;
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            directionName = Constants.LANE_LEFT;
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            directionName = Constants.LANE_RIGHT;
        }
        else
            directionName = Constants.INVALID_SWIPE;

        OnTouchPerformed?.Invoke(directionName);
    }
}
