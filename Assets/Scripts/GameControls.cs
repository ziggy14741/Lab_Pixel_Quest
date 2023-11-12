using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour
{

    public string nextLevelName = "TestLevel";
    public string currentLevelName = "TestLevel";
    private GameObject _fadeObject;
    private Animator _animator;

    private void Start()
    {
        if (GameObject.Find("Canvas"))
        {
            _fadeObject = GameObject.Find("Fade");
            _fadeObject.SetActive(false);
            _animator = _fadeObject.GetComponent<Animator>();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(WaitForFade(nextLevelName));
    }

    public void ReloadLevel()
    {
        StartCoroutine(WaitForFade(currentLevelName));
    }

    public void LoadMenu()
    {
        StartCoroutine(WaitForFade("MenuLevel"));
    }

    private IEnumerator WaitForFade(string levelName)
    {
        _fadeObject.SetActive(true);
        _animator.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelName);
    }
}
