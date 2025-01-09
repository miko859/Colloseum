using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathScreenFade : MonoBehaviour
{
    public Health health;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 2.0f;
    private bool isFading = false;

    public void TriggerDeathFade()
    {
        if (!isFading)
        {
            StartCoroutine(FadeToDeath());
        }
    }

    private IEnumerator FadeToDeath()
    {
        isFading = true;
        float elapsedTime = 0f;
        FindAnyObjectByType<PlayerInput>().DeactivateInput();

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeCanvasGroup.alpha = alpha;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
        isFading = false;
        health.SetIsAlive(false);

        
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        FindAnyObjectByType<PlayerInput>().ActivateInput();
    }
}