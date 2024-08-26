using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform cameraTransform; // Kamera hr��a
    public float interactionRange = 5f; // Dosah interakcie
    public LayerMask interactionLayerMask; // Vrstva pre interakt�vne objekty

    private Interactable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact(); // Spust� interakciu
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
                // Zobraz� prompt, ak je k dispoz�cii
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