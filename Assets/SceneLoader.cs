using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen; // Reference to the loading screen GameObject
    public Slider progressBar; // Reference to the slider (progress bar)

    public void LoadGameScene()
    {
        Debug.Log("Loading game scene...");
        loadingScreen.SetActive(true); // Ensure loading screen stays active
        StartCoroutine(LoadAsyncScene());
    }

    private IEnumerator LoadAsyncScene()
    {
        Debug.Log("Preparing to load scene...");
        AsyncOperation operation = SceneManager.LoadSceneAsync("Desert");
        operation.allowSceneActivation = false; // Hold scene activation until fully loaded
        Debug.Log("Scene loading initiated.");

        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            Debug.Log($"Loading progress: {operation.progress}");
            yield return null;
        }

        progressBar.value = 1f; // Loading complete
        Debug.Log("Scene fully loaded.");

        // Set allowSceneActivation to true to trigger scene switch
        operation.allowSceneActivation = true;
    }
}
