using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public Slider progressBar;
    public float fakeLoadingSpeed = 0.1f; // Speed for smooth progress 
    public Image image1;
    public Image image2;
    public Image blackScreen;
    public float fadeDuration = 1f;
    private bool isFirstImageShowing = true;
    public float delay = 2f;

    private void Start()
    {
        StartCoroutine(SwitchImagesWithDelay());
        StartCoroutine(LoadDesertSceneAsync());
    }
    public void StartImageSwitch()
    {
        StartCoroutine(SwitchImages());
    }
    private IEnumerator SwitchImagesWithDelay()
    {
        yield return new WaitForSeconds(2f);

        StartImageSwitch();
    }

    private IEnumerator SwitchImages()
    {
        yield return StartCoroutine(FadeImage(blackScreen, 0f, 1f, fadeDuration));

        if (isFirstImageShowing)
        {
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(true);
        }
        else
        {
            image1.gameObject.SetActive(true);
            image2.gameObject.SetActive(false);
        }
        isFirstImageShowing = !isFirstImageShowing; // switch between images

        yield return StartCoroutine(FadeImage(blackScreen, 1f, 0f, fadeDuration));

    }
    private IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color imageColor = image.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            imageColor.a = alpha;
            image.color = imageColor;
            yield return null;
        }

        imageColor.a = endAlpha;
        image.color = imageColor;

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