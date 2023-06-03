using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

[DefaultExecutionOrder(-2)]
public class PlayerInputManager : Singleton<PlayerInputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += FingerDown;
        Touch.onFingerUp += FingerUp;
    }
    private void OnDisable()
    {
        Touch.onFingerDown -= FingerDown;
        Touch.onFingerUp -= FingerUp;
        EnhancedTouchSupport.Disable();
    }
    private void FingerDown(Finger finger) => OnStartTouch?.Invoke(finger.currentTouch.startScreenPosition, (float)finger.currentTouch.startTime);
    private void FingerUp(Finger finger) => OnEndTouch?.Invoke(finger.currentTouch.screenPosition, (float)finger.currentTouch.time);
}
