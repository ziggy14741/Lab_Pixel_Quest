using UnityEngine;

public class SoundEffectDeath : MonoBehaviour {
    
    // =================================================================================================================
    // VARIABLES 
    // =================================================================================================================
    
    private AudioSource _audioSource; // AudioSource the will play
    
    // =================================================================================================================
    // METHODS  
    // =================================================================================================================

    public void SetAudio(AudioSource audioSource) {
        _audioSource = GetComponent<AudioSource>();
        
        _audioSource.clip = audioSource.clip;           // Copy the audio clip
        _audioSource.volume = audioSource.volume;       // Copy the volume 
        _audioSource.Play();                            // Play the SFX 
        Invoke($"DestroyAfterAudio", _audioSource.clip.length); // Wait till the the clip lenght pasted and Destroy this object 
    }
    
    // Destroy the GameObject
    private void DestroyAfterAudio() { Destroy(gameObject); }
}
