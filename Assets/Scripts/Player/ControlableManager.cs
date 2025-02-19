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
    public IControlable currentControllable;

    //Events
    public event Action onPlayerDeath;
    public event Action onSubDestruction;

    private void Start()
    {
        SubscribeToInputEvents();
        SubscribeToControllerEvents();

        EnterSub();
    }

    private void Update()
    {
        UpdateControlable();
    }

    void UpdateControlable()
    {
        currentControllable.UpdateControlable(inputManager.moveDirection, inputManager.mouseWorldPosition);
    }

    private void ChangeCurrentControlable(IControlable controlable)
    {
        currentControllable = controlable;
        cam.SetTarget(currentControllable.GetCameraFollow());
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
            player.player.transform.position = sub.playerSpawn.position;
            player.player.SetActive(true);
            player.mover.ApplyForce(sub.mover.GetCurrentVelocity());
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

        sub.onSubDestroy += SubDestruction;
        player.onDeath += onPlayerDeath;
    }

    void SubscribeToInputEvents()
    {
        inputManager.OnPressed_E += player.interactor.TryInteraction;
        inputManager.OnPressed_Q += ExitSub;

        inputManager.OnPressed_Shift += player.Run;
        inputManager.OnPressed_Shift += sub.Run;
        inputManager.OnLifted_Shift += player.Walk;
        inputManager.OnLifted_Shift += sub.Walk;
        inputManager.OnPressed_Space += player.Space;
    }
}
