using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform cameraTransform; // Kamera hráèa
    public float interactionRange; // Dosah interakcie
    public LayerMask interactionLayerMask; // Vrstva pre interaktívne objekty

    public Interactable currentInteractable;

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

        //if (Physics.Raycast(ray, out hit, interactionRange, interactionLayerMask))
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, interactionRange))
        {
            Debug.DrawLine(cameraTransform.position, cameraTransform.forward * hit.distance, Color.red);

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
            Debug.DrawLine(cameraTransform.position, cameraTransform.forward * interactionRange, Color.blue);
            currentInteractable = null;
        }
    }
}