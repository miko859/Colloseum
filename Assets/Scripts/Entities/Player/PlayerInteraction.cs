using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform cameraTransform; 
    public float interactionRange; 
    public LayerMask interactionLayerMask;

    public Interactable currentInteractable;

    void Update()
    {
        CheckForInteractable();
    }

    public void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionRange))
        {
            Debug.DrawLine(cameraTransform.position, cameraTransform.forward * hit.distance, Color.red);

            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                Debug.Log(interactable.GetInteractPrompt());
            }
            else
            {
                currentInteractable = null;
            }
        }
        else
        {
            Debug.DrawLine(cameraTransform.position, cameraTransform.forward * interactionRange, Color.blue);
            currentInteractable = null;
        }
    }
}