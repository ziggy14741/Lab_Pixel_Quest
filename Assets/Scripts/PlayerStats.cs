using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    
    // =================================================================================================================
    // VARIABLES 
    // =================================================================================================================
    [HideInInspector] public int coinCounter = 0;          // Keeps track of how many coins the player has collected 
    [HideInInspector] public int lives = 3;                // Keeps track of how many lives the player has 
    [HideInInspector] public bool immobile = false;        // Tells us if we the player can move 
    [HideInInspector] public bool invulnerable = false;    // Tells us if the player cannot be hurt 

    [HideInInspector] public GameObject respawnPoint;      // Tells us where the respawn point is located 
    
    private GameObject _victoryUI;                         // UI for Victory Screen 
    private GameObject _gameOverUI;                        // UI for Game Over Screen
    private GameObject _levelUI;                           // UI during the game 
    private TextMeshProUGUI _heartText;                    // Holds text information for player lives 
    private TextMeshProUGUI _coinText;                     // Holds text information for coin count 

    // =================================================================================================================
    // METHODS  
    // =================================================================================================================
    private void Start() {
        // Save the placement of the respawn point 
        respawnPoint = GameObject.Find("RespawnPoint");
        
        // Checks if the given UI elements exit, if they do connect
        
        if (GameObject.Find("VictoryUI")) {
            _victoryUI = GameObject.Find("VictoryUI");
            _victoryUI.SetActive(false);   
        }

        if (GameObject.Find("GameOverUI")) {
            _gameOverUI = GameObject.Find("GameOverUI");
            _gameOverUI.SetActive(false);
        }

        if (GameObject.Find("LevelUI")) {
            _levelUI = GameObject.Find("LevelUI");
        }

        if (GameObject.Find("HeartText")) {
            _heartText = GameObject.Find("HeartText").GetComponent<TextMeshProUGUI>();
        }
        
        if (GameObject.Find("CoinText")) {
            _coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        }
        
        UpdateLivesText();
        UpdateCoinText();
    }

    // Updates the player lives text, the value was update inside Player Triggers 
    public void UpdateLivesText() {
        if (_heartText != null) {
            _heartText.text = lives.ToString();
        }
    }

    // Updates the coin text, the value was update inside Player Triggers 
    public void UpdateCoinText() {
        if (_coinText != null) {
            _coinText.text = coinCounter.ToString();
        }
    }

    // Updates the game to turn off the level UI and turn on the game over UI 
    public void GameOver() {
        if (_gameOverUI != null) {
            _gameOverUI.SetActive(true);
        }
        if (_levelUI != null) {
            _levelUI.SetActive(false);
        }
    }

    // Updates the game to turn off the level UI and turn on the game UI 
    public void Victory() {
        immobile = true;
        if (_victoryUI != null) {
            _victoryUI.SetActive(true);
        }
        if (_levelUI != null) {
            _levelUI.SetActive(false);
        }
    }
}
