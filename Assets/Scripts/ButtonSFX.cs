using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    private AudioSource buttonSFX;

    private void Start()
    {
        if (GetComponent<AudioSource>())
        {
            buttonSFX = GetComponent<AudioSource>();
        }
    }

    public void PlayButtonSFX()
    {
        if (GetComponent<AudioSource>())
        {
            buttonSFX.Play();
        }
    }
}
