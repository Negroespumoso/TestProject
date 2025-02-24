using UnityEngine;
using System;

public class SubManager : MonoBehaviour, IInteractable, IControlable
{
    [Header("Components")]
    public Mover mover;
    [SerializeField] private MonoBehaviour subLight;
    [SerializeField] private HealthManager health;
    [SerializeField] private Transform camFollow;
    [SerializeField] public float camZoom;

    //Variables
    [HideInInspector] public Vector2 mousePosition;
    [SerializeField] private float swimSpeed;
    [SerializeField] private float fastSwimSpeed;
    [SerializeField] private float lightTurnSpeed;

    //Events
    public event Action onSubenter;
    public event Action onSubDestroy;


    void Start()
    {
        health.onHealthDrained += Die;
        SetUpMover();
    }

    public void UpdateControlable(Vector2 moveDirection, Vector2 currentMosePosition)
    {
        mover.moveDirection = moveDirection;
        mousePosition = currentMosePosition;
    }
    public void SetUpMover()
    {
        mover.defaultSpeed = swimSpeed;
        mover.Init();
    }
    public Transform GetCameraFollow()
    {
        return camFollow;
    }
    public float GetCameraZoom()
    {
        return camZoom;
    }

    public void Run()
    {
        mover.ChangeMovespeed(fastSwimSpeed);
    }
    public void Walk()
    {
        mover.ChangeMovespeed(swimSpeed);
    }

    public void Interact(GameObject interactor)
    {
        onSubenter?.Invoke();
    }

    public void Hit(float damage)
    {
        health.SubstractHealth(damage);
    }

    private void Die()
    {
        onSubDestroy?.Invoke();
    }
}
