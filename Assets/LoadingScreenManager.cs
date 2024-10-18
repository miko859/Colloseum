using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public Slider progressBar;
    public float fakeLoadingSpeed = 0.1f; // Speed for smooth progress 

    private void Start()
    {
        StartCoroutine(LoadDesertSceneAsync());
    }

    private IEnumerator LoadDesertSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Desert");
        operation.allowSceneActivation = false;

        float fakeProgress = 0f;

        while (operation.progress < 0.9f || fakeProgress < 0.7f)
        {
            // Fake progress to make the slider smoother
            if (fakeProgress < 1f)
            {
                fakeProgress = Mathf.MoveTowards(fakeProgress, operation.progress / 0.9f, fakeLoadingSpeed * Time.deltaTime);
                progressBar.value = fakeProgress;
            }
            else
            {

                progressBar.value = Mathf.Clamp01(operation.progress / 0.9f);
            }

            yield return null;
        }

        progressBar.value = 1f;

        operation.allowSceneActivation = true;
    }
}
