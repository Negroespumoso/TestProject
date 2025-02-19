using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float defaultSmoothTime;
    private float smoothTime;
    private Vector3 velocity = new Vector3(0, 0);

    public Transform followTarget;

    private void Start()
    {
        SetSmoothTime(defaultSmoothTime);
    }

    void FixedUpdate()
    {
        Follow();
    }

    public void Follow()
    {
        Vector3 targetPos = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }

    public void SetTarget(Transform target)
    {
        followTarget = target;
    }

    public void SetSmoothTime(float sTime)
    {
        smoothTime = sTime;
    }


}
