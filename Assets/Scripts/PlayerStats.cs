using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Transform respawnPoint;
    public int playerLife = 3;

    private Rigidbody2D rigidbody2D; 

    private const string deathTag = "Death";

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
                    break;
                }
        }
    }

}
