using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform cameraTransform; // Kamera hráèa
    public float interactionRange = 5f; // Dosah interakcie
    public LayerMask interactionLayerMask; // Vrstva pre interaktívne objekty

    private Interactable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact(); // Spustí interakciu
        }
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactionLayerMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                // Zobrazí prompt, ak je k dispozícii
                Debug.Log(interactable.GetInteractPrompt());
            }
            else
            {
                currentInteractable = null;
            }
        }
        else
        {
            currentInteractable = null;
        }
    }
}