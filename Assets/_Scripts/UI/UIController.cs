using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void PlaySound()
    {
        if (!AudioManager.Instance.ReturnAudioSource("Click").isPlaying)
        {
            AudioManager.Instance.PlayOneShot("Click");
        }
    }
}
