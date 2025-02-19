using UnityEngine;

public class RotateToMousePosition : MonoBehaviour
{
    public Vector2 lookDirection;

    public void UpdateLookDirection(Vector2 mousePosition)
    {
        lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y).normalized;

        float angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
}
