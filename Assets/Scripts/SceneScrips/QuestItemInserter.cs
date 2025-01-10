using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class QuestItemInserter : Interactable
{
    public CanvasGroup blackFadeCanvas;
    public CanvasGroup endingTextCanvas;
    public CanvasGroup[] playersUI;
    public int fadeDuration;
    [SerializeField] public Item neededItem;

    public override void Interact()
    {
        Item questItem = InventoryManager.Instance.items.Find(item => item == neededItem);

        if (questItem != null)
        {
            InventoryManager.Instance.items.Remove(questItem);
            interactPrompt = "";
            StartCoroutine(StartEndingScreen());
        }
        else
        {
            Debug.Log("nenašlo srdce");
        }
    }

    private IEnumerator StartEndingScreen()
    {
        float elapsedTimeBg = 0f;
        float elapsedTimeText = 0f;
        FindAnyObjectByType<PlayerInput>().DeactivateInput();

        while (elapsedTimeBg < fadeDuration)
        {
            elapsedTimeBg += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTimeBg / fadeDuration);
            blackFadeCanvas.alpha = alpha;
            yield return null;

            foreach (CanvasGroup canvas in playersUI)
            {
                canvas.alpha = 1 - alpha;
                yield return null;
            }
        }

        blackFadeCanvas.alpha = 1f;

        while (elapsedTimeText < fadeDuration)
        {
            elapsedTimeText += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTimeText / fadeDuration);
            endingTextCanvas.alpha = alpha;
            yield return null;
        }

        endingTextCanvas.alpha = 1f;

        yield return new WaitForSeconds(3);

        elapsedTimeText = 0f;
        while (elapsedTimeText < fadeDuration)
        {
            elapsedTimeText += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTimeText / fadeDuration);
            endingTextCanvas.alpha = alpha;
            yield return null;
        }

        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        FindAnyObjectByType<PlayerInput>().ActivateInput();
    }
}