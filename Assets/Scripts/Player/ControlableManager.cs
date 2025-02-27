using UnityEngine;
using System;

public class ControlableManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CameraFollow cam;


    [Header("Controlables")]
    [SerializeField] private PlayerManager player;
    [SerializeField] private SubManager sub;

    //Variables
    public Controlable currentControllable;

    //Events
    public event Action onPlayerDeath;
    public event Action onSubDestruction;

    private void Start()
    {
        SubscribeToInputEvents();
        SubscribeToControllerEvents();

        ExitSub();
    }

    private void Update()
    {
        UpdateControlable();
    }

    void UpdateControlable()
    {
        currentControllable.UpdateControlable(inputManager.moveDirection, inputManager.mouseWorldPosition);
    }

    private void ChangeCurrentControlable(Controlable controlable)
    {
        currentControllable = controlable;
        cam.SetTarget(currentControllable.camFollow, currentControllable.camZoom);
    }

    void EnterSub()
    {
        ChangeCurrentControlable(sub);
        player.EnterSub();
    }

    void ExitSub()
    {   
        if(currentControllable == sub)
        {
            ChangeCurrentControlable(player);
            player.flashlightRotator.gameObject.SetActive(true);
            player.mover.ApplyForce(sub.mover.GetCurrentVelocity());
            player.followTransform.isActive = false;
        }
    }
    void SubDestruction()
    {
        player.Hit(1000000000);
        onSubDestruction?.Invoke();
    }

    void SubscribeToControllerEvents()
    {
        sub.onSubenter += EnterSub;

        sub.onDeath += SubDestruction;
        player.onDeath += onPlayerDeath;
    }

    void SubscribeToInputEvents()
    {
        inputManager.OnPressed_E += player.interactor.TryInteraction;
        inputManager.OnPressed_Q += ExitSub;
        inputManager.OnPressed_F += currentControllable.SwapLight;

        inputManager.OnPressed_Shift += player.Run;
        inputManager.OnPressed_Shift += sub.Run;
        inputManager.OnLifted_Shift += player.Walk;
        inputManager.OnLifted_Shift += sub.Walk;
        inputManager.OnPressed_Space += player.Space;
    }
}
