using UnityEngine;

public interface IControlable
{
    void UpdateControlable(Vector2 moveDirection, Vector2 currentMouseposition);
    void Run();
    void Walk();
    void SetUpMover();
    Transform GetCameraFollow();
}
