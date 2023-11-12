using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public int coinCounter = 0;
    [HideInInspector] public int lives = 3;
    [HideInInspector] public bool immoblie = false;
    [HideInInspector] public bool invurnrable = false;
    [HideInInspector] public bool isPoweredUp = false;

    [HideInInspector] public GameObject respawnPoint;
    private GameObject _victoryUI;
    private GameObject _gameOverUI;
    private GameObject _levelUI;
    private TextMeshProUGUI _heartText;
    private TextMeshProUGUI _coinText;

    private void Start()
    {
        respawnPoint = GameObject.Find("RespawnPoint");

        if (GameObject.Find("Canvas"))
        {
            _victoryUI = GameObject.Find("VictoryUI");
            _victoryUI.SetActive(false);

            _gameOverUI = GameObject.Find("GameOverUI");
            _gameOverUI.SetActive(false);

            _levelUI = GameObject.Find("LevelUI");

            _heartText = GameObject.Find("HeartText").GetComponent<TextMeshProUGUI>();
            _coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();

            UpdateLivesText();
            UpdateCoinText();
        }
    }

    public void UpdateLivesText()
    {
        if (GameObject.Find("Canvas"))
        {
            _heartText.text = lives.ToString();
        }
    }

    public void UpdateCoinText()
    {
        if (GameObject.Find("Canvas"))
        {
            _coinText.text = coinCounter.ToString();
        }
    }

    public void GameOver()
    {
        if (GameObject.Find("Canvas"))
        {
            _gameOverUI.SetActive(true);
            _levelUI.SetActive(false);
        }
    }

    public void Victory()
    {
        immoblie = true;
        if (GameObject.Find("Canvas"))
        {
            _victoryUI.SetActive(true);
            _levelUI.SetActive(false);
        }
    }
}
