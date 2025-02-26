using UnityEngine;
using System;

public class SubManager : Controlable, IInteractable
{

    //Events
    public event Action onSubenter;

    public void Interact(GameObject interactor)
    {
        onSubenter?.Invoke();
    }
}
