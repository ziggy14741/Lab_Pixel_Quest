using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public Transform respawnPoint;
    public int playerLife = 3;
    public int currentCoins = 0;

    private Rigidbody2D rigidbody2D; 

    private const string deathTag = "Death";
    private const string healthTag = "Health";
    private const string coinTag = "Coin";
    private const string respawnTag = "Respawn";
    private const string finishTag = "Finish";

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag){
            case deathTag:
                {
                    rigidbody2D.velocity = Vector2.zero;
                    transform.position = respawnPoint.position;
                    playerLife--;
                    if (playerLife <= 0)
                    {
                        string currentLevel = SceneManager.GetActiveScene().name;
                        SceneManager.LoadScene(currentLevel);
                    }
                    break;
                }
            case healthTag:
                {
                    playerLife++;
                    Destroy(collision.gameObject);
                    break;
            }
            case coinTag:
                {
                    currentCoins++;
                    Destroy(collision.gameObject);
                    break;
                }
            case respawnTag:
                {
                    respawnPoint.position = collision.transform.position;
                    break;
                }
            case finishTag:
                {
                    SceneManager.LoadScene("Level_2");
                    break;
                }
        }
    }

}
