using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Vector2 lookDirection;

    [SerializeField] private float interactRange;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private GameObject interactText;

    public void TryInteraction()
    {
        RaycastHit2D r = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), lookDirection, interactRange, interactableLayerMask);

        if (r.collider != null)
        {
            if (r.collider.gameObject.TryGetComponent(out IInteractable interactObject))
            {
                interactObject.Interact();
                DeactivateText();
            }
        }
    }

    public void UpdateLookDirection(Vector3 mousePosition)
    {
        lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
    }

    private void Update()
    {
        RaycastHit2D r = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), lookDirection, interactRange, interactableLayerMask);
        if (r.collider != null)
        {
            if (r.collider.gameObject.TryGetComponent(out IInteractable interactObject)) ActivateText();            
        }
        else DeactivateText();
    }

    private void ActivateText()
    {
        interactText.SetActive(true);
    }
    private void DeactivateText()
    {
        interactText.SetActive(false);
    }
}
