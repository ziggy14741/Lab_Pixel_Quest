using UnityEngine;

public class ButtonSfx : MonoBehaviour {
    
    // =================================================================================================================
    // VARIABLES 
    // =================================================================================================================
    
    private AudioSource _buttonSfx;

    // =================================================================================================================
    // METHODS  
    // =================================================================================================================
    
    // Get access to the audio source 
    private void Start() { if (GetComponent<AudioSource>()) { _buttonSfx = GetComponent<AudioSource>(); } }

    // Plays the audio source upon button click 
    public void PlayButtonSfx() { if (GetComponent<AudioSource>()) { _buttonSfx.Play(); } }
}
