using UnityEngine;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen; 

    private void Start()
    {
        loadingScreen.SetActive(true); 
        StartCoroutine(DeactivateLoadingScreen());
    }

    private IEnumerator DeactivateLoadingScreen()
    {
        yield return new WaitForSeconds(1f); 
        loadingScreen.SetActive(false);
    }
}
