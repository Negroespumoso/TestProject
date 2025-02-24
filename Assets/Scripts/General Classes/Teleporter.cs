using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    [SerializeField] Transform tpLocation;

    public void Interact(GameObject interactor)
    {
        interactor.transform.position = tpLocation.position;
    }
}
