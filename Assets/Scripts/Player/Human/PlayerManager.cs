using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerManager : Controlable, IHittable
{
    [Header("Components")]
    [SerializeField] public Interactor interactor;
    [SerializeField] public RotateToMousePosition flashlightRotator;
    public FollowTransform followTransform;

    [Header("Variables")]
    public bool isInWater;
    [SerializeField] float flashlightSpeed;

    [Header("Swimming")]
    [SerializeField] float airCapacity;
    [SerializeField] Slider airSlider;
    [SerializeField] float swimSpeed;
    [SerializeField] float fastSwimSpeed;

    private float currentAir;
    private float airDrainMultiplier;

    [SerializeField] float dashForce;
    [SerializeField] private float dashCooldown;
    private float dashCounter;

    [Header("Walk")]
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private Transform jumpCheck;
    [SerializeField] private float jumpRange;
    [SerializeField] private LayerMask jumpMask;
    private bool isGrounded;


    void Start()
    {
        currentAir = airCapacity;
        SetAirDrainSpeed(1);
        health.onHealthDrained += Die;
        SetUpMover();
        SwapState(true);
        SetMaximumAir(airCapacity);
    }

    public override void UpdateControlable(Vector2 moveDirection, Vector2 currentMosePosition)
    {
        base.UpdateControlable(moveDirection, currentMosePosition);
        UpdateMover(moveDirection);
        UpdateMousePosition(currentMosePosition);

        if (isInWater) DrainAir(airDrainMultiplier * Time.deltaTime);

        UpdateDash();
        CheckGrounded();
    }

    private void UpdateMover(Vector2 moveDirection)
    {
        if (isInWater)
        {
            mover.moveDirection = moveDirection;
        }
        else
        {
            mover.moveDirection = new Vector2(moveDirection.x, 0);
            if (moveDirection == Vector2.zero && isGrounded) mover.ApplyFriction(true, 0.2f);
        }
    }
    private void UpdateMousePosition(Vector2 currentMosePosition)
    {
        mousePosition = currentMosePosition;
        flashlightRotator.UpdateLookDirection(mousePosition, flashlightSpeed);
        interactor.UpdateLookDirection(mousePosition);
    }

    public void SwapState(bool inWater)
    {
        if (inWater)
        {
            isInWater = true;
            mover.rb.gravityScale = 0;
            mover.rb.linearDamping = 1.5f;
            normalSpeed = swimSpeed;
            fastSpeed = fastSwimSpeed;
        }
        else
        {
            FillAir(airCapacity);
            isInWater = false;
            mover.rb.gravityScale = 1;
            mover.rb.linearDamping = 2;
            normalSpeed = walkSpeed;
            fastSpeed = runSpeed;
        }
        mover.ChangeMovespeed(normalSpeed);
    }

    #region AirControl
    public void DrainAir(float amount)
    {
        currentAir -= amount;
        airSlider.value = currentAir;
        if (currentAir <= 0) Die();
    }
    public void FillAir(float amount)
    {
        currentAir += amount;
        airSlider.value = currentAir;
        if (currentAir > airCapacity) currentAir = airCapacity;
    }
    public void SetAirDrainSpeed(float airLossPerSecond)
    {
        airDrainMultiplier = airLossPerSecond;
    }
    public void SetMaximumAir(float amount)
    {
        airCapacity = amount;
        airSlider.maxValue = amount;
    }
    #endregion

    #region Space

    public void Space()
    {
        if (isInWater) Dash();
        else Jump();
    }

    private void CheckGrounded()
    {
        RaycastHit2D r = Physics2D.Raycast(jumpCheck.position, -transform.up, jumpRange, jumpMask);
        if (r.collider != null) isGrounded = true;
        else isGrounded = false;
    }
    private void Jump()
    {
        if (isGrounded)
        {
            mover.ApplyForce(transform.up * jumpForce);
        }
    }

    private void Dash()
    {
        if (dashCounter <= 0 && mover.moveDirection != Vector2.zero)
        {
            mover.ApplyForce(mover.moveDirection * dashForce);
            dashCounter = dashCooldown;
        }
    }

    void UpdateDash()
    {
        if (dashCounter > 0) dashCounter -= Time.deltaTime;
    }
    #endregion

    public void EnterSub()
    {
        flashlightRotator.gameObject.SetActive(false);
        followTransform.isActive = true;
    }
}
