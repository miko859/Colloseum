using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public Slider progressBar;
    public float fakeLoadingSpeed = 0.1f; // Speed for smooth progress 
    public Image backgroundImage; 
    public Sprite[] backgroundSprites; // Array for bakcground images 
    public float backgroundSwitchInterval = 2f;

    private void Start()
    {
        StartCoroutine(LoadDesertSceneAsync());
        StartCoroutine(SwitchBackgroundImages());
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
    private IEnumerator SwitchBackgroundImages()
    {
        int index = 0;
        while (true)
        {
            if (backgroundSprites.Length > 0)
            {
                Debug.Log("Switching to background image: " + backgroundSprites[index].name); // Log the name of the sprite being set
                backgroundImage.sprite = backgroundSprites[index]; 
                index = (index + 1) % backgroundSprites.Length; // loops back to the first image 
            }

            yield return new WaitForSeconds(backgroundSwitchInterval); 
        }
    }
}