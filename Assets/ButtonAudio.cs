using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hover;
    public AudioClip click;

    public void hoverSound()
    {
        audioSource.PlayOneShot(hover);
    }

    public void clickSound()
    {
        audioSource.PlayOneShot(click);
    }


}
