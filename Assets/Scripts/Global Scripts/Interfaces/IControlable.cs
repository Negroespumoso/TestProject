using UnityEngine;
using System;

public class Controlable : MonoBehaviour
{
     [Header("Components")]
    public Mover mover;
    [SerializeField] protected HealthManager health;
    public Transform camFollow;
    public float camZoom;
    [SerializeField] protected LightManager flashLight;

    //Variables
    [HideInInspector] public Vector2 mousePosition;
    [SerializeField] protected float normalSpeed;
    [SerializeField] protected float fastSpeed;

    //Events
    public event Action onDeath;


    void Start()
    {
        health.onHealthDrained += Die;
        SetUpMover();
    }
    public virtual void UpdateControlable(Vector2 moveDirection, Vector2 currentMosePosition)
    {
        mover.moveDirection = moveDirection;
        mousePosition = currentMosePosition;
    }

    public void SetUpMover()
    {
        mover.defaultSpeed = normalSpeed;
        mover.Init();
    }

    public void SwapLight()
    {
        flashLight.SwapLight();
    }
    public void Run()
    {
        mover.ChangeMovespeed(fastSpeed);
    }
    public void Walk()
    {
        mover.ChangeMovespeed(normalSpeed);
    }

    public void Hit(float damage)
    {
        health.SubstractHealth(damage);
    }

    protected void Die()
    {
        onDeath?.Invoke();
    }
}
