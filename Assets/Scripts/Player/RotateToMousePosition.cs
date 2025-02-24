using UnityEngine;

public class RotateToMousePosition : MonoBehaviour
{
    [HideInInspector] public Vector2 lookDirection;
    private float followSpeed;

    public void UpdateLookDirection(Vector2 mousePosition)
    {
        lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;

        float angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }

    public void UpdateLookDirection(Vector2 mousePosition, float speed)
    {
        lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;
        followSpeed = speed;
    }

    private void Update()
    {
        if(followSpeed > 0)
        {
            float targetAngle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
            Quaternion lookRotation = Quaternion.Euler(new Vector3(0, 0, -targetAngle));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, followSpeed * Time.deltaTime);
        }
    }
}
