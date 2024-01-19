using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour {
    
    // =================================================================================================================
    // VARIABLES 
    // =================================================================================================================
   
    public string nextLevelName = "TestLevel";      // Name of the next level the player will go to
    private GameObject _fadeObject;                 // Fade
    private Animator _animator;

    // =================================================================================================================
    // METHODS  
    // =================================================================================================================
    private void Start() {
        if (!GameObject.Find("Fade")) return;
        _fadeObject = GameObject.Find("Fade");
        _animator = _fadeObject.GetComponent<Animator>();
        _fadeObject.SetActive(false);
    }

    // Used but UI button calls 
    public void LoadNextLevel() { StartCoroutine(WaitForFade(nextLevelName)); }
    public void ReloadLevel() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    public void LoadMenu() { StartCoroutine(WaitForFade("MenuLevel")); }

    // Waits turns on the fade object, waits till screen gets dark and then loads the level 
    private IEnumerator WaitForFade(string levelName)
    {
        if (_fadeObject != null) {
            _fadeObject.SetActive(true);
            _animator.Play("FadeOut");   
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelName);
    }
}
