using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public Transform respawnPoint;
    public float playerLife = 3;
    public int currentCoins = 0;

    private Rigidbody2D rigidbody2D; 

    private const string deathTag = "Death";
    private const string healthTag = "Health";
    private const string coinTag = "Coin";
    private const string respawnTag = "Respawn";
    private const string finishTag = "Finish";

    public Image heartImage;
    private float playerMaxHealth = 3;

    public TextMeshProUGUI coinText;
    public GameObject CoinParent;
    private int maxCoins;

    public AudioSource deathSFX;
    public AudioSource coinSFX;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        maxCoins = CoinParent.transform.childCount;
        coinText.text = currentCoins + "/" + maxCoins;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag){
            case coinTag:
                {
                    coinSFX.Play();
                    currentCoins++;
                    coinText.text = currentCoins + "/" + maxCoins;
                    Destroy(collision.gameObject);
                    break;
                }
            case deathTag:
                {
                    deathSFX.Play();
                    rigidbody2D.velocity = Vector2.zero;
                    transform.position = respawnPoint.position;
                    playerLife--;
                    heartImage.fillAmount = playerLife / playerMaxHealth;
                    if (playerLife <= 0)
                    {
                        string currentLevel = SceneManager.GetActiveScene().name;
                        SceneManager.LoadScene(currentLevel);
                    }
                    break;
                }
            case healthTag:
                {
                    if(playerLife < playerMaxHealth)
                    {
                        playerLife++;
                        heartImage.fillAmount = playerLife / playerMaxHealth;
                        Destroy(collision.gameObject);
                    }
                    break;
            }
            case respawnTag:
                {
                    respawnPoint.position = collision.transform.Find("RespawnPoint").position;
                    break;
                }
            case finishTag:
                {
                    string nextLevel = collision.gameObject.GetComponent<GameEnd>().nextLevel;
                    SceneManager.LoadScene(nextLevel);
                    break;
                }
        }
    }

}
