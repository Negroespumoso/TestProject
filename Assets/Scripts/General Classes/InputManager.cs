using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public Vector2 moveDirection;

    public Vector3 mouseWorldPosition;

    [SerializeField] private Camera mainCamera;

    //Key Events
    public event Action OnPressed_E;
    public event Action OnPressed_Q;

    public event Action OnPressed_Space;
    public event Action OnPressed_Shift;
    public event Action OnLifted_Shift;

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.E)) OnPressed_E?.Invoke();

        if (Input.GetKeyDown(KeyCode.Q)) OnPressed_Q?.Invoke();

        if (Input.GetKeyDown(KeyCode.Space)) OnPressed_Space?.Invoke();

        if (Input.GetKeyDown(KeyCode.LeftShift)) OnPressed_Shift?.Invoke();
        if (Input.GetKeyUp(KeyCode.LeftShift)) OnLifted_Shift?.Invoke();

        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);        
    }
}
