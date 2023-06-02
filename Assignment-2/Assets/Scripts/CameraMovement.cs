using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float smooth = 0.1f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;

    private void Start() => offset = transform.position - target.position;

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smooth * Time.deltaTime);
    }
}
