using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int xMul = 3;
    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    // Moves player and flips 
    void Update()
    {
        // Gets player info 
        float xVelocity = Input.GetAxis("Horizontal");

        // Flips player 
        if (xVelocity > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false; 
        }


        // Save player info to rigid
        rigidbody2D.velocity = new Vector2 (xMul * xVelocity, rigidbody2D.velocity.y);

    }
}
