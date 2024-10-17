using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen; // Reference to the loading screen GameObject
    public Slider progressBar; // Reference to the slider (progress bar)
    public float delay = 0.5f; 

    public void LoadGameScene()
    {
        Debug.Log("Loading game scene..."); 
        loadingScreen.SetActive(true);
        Debug.Log("Starting LoadAsyncScene coroutine...");
        StartCoroutine(LoadAsyncScene());
    }

    private IEnumerator LoadAsyncScene()
    {
        Debug.Log("Preparing to load scene...");
        AsyncOperation operation = SceneManager.LoadSceneAsync("Desert"); 
        operation.allowSceneActivation = false;
        Debug.Log("Scene loading initiated.");

        while (operation.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            Debug.Log($"Loading progress: {operation.progress}");
            yield return null;
        }

        progressBar.value = 1f; 

        StartCoroutine(DelayedSceneActivation(operation));
    }

    private IEnumerator DelayedSceneActivation(AsyncOperation operation)
    {
        Debug.Log("About to start delay...");
        yield return new WaitForSeconds(delay); 

        Debug.Log("Activating scene...");
        operation.allowSceneActivation = true;
    }
}
