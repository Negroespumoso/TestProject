using UnityEngine;
using System.Collections;
using System;

public class Mover : MonoBehaviour
{
    public Rigidbody2D rb;

    private float moveSpeed;
    [HideInInspector] public Vector2 moveDirection;

    public event Action OnOrientationFlip;
    private float lastOrientation;

    private float stunCounter;
    private bool stunned;
    [HideInInspector] public float defaultSpeed;
    private Vector2 frictionForce;

    public void Init()
    {
        lastOrientation = transform.localScale.y;
        OnOrientationFlip += FlipOrientation;
        moveSpeed = defaultSpeed;
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!stunned)
        {
            //Check Look Orientation
            if (moveDirection.x > 0 && lastOrientation < 0) OnOrientationFlip?.Invoke();
            else if (moveDirection.x < 0 && lastOrientation > 0) OnOrientationFlip?.Invoke();

            if (moveDirection.x != 0) lastOrientation = moveDirection.x;

            //Move
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Force);
            //Add Friction
            rb.AddForce(frictionForce, ForceMode2D.Impulse);
            frictionForce = Vector2.zero;
        }
        else
        {
            if(stunCounter > 0) stunCounter -= Time.deltaTime;
            else stunned = false;
        }

        moveDirection = Vector2.zero;
    }

    public void ApplyFriction(bool horizontal, float friction)
    {
        if (horizontal) frictionForce += new Vector2(-rb.linearVelocity.x * friction, 0);
        else frictionForce +=  new Vector2(0, -rb.linearVelocity.x * friction);
    }

    public void ChangeMovespeed(float speed)
    {
        moveSpeed = speed;
    }

    public void Stun(float time)
    {
        stunCounter = time;
        stunned = true;
    }

    public void ApplyForce(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public Vector2 GetCurrentVelocity()
    {
        return rb.linearVelocity;
    }

    void FlipOrientation()
    {
        transform.RotateAround(transform.position, Vector3.up, 180f);        
    }
}
