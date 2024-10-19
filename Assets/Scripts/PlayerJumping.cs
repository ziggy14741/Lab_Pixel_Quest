using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float JumpForce = 4;
    public Transform ground;
    public LayerMask groundMask;

    private bool waterCheck; 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool groundCheck = Physics2D.OverlapCapsule(ground.position, new Vector2(1, 0.08f), CapsuleDirection2D.Horizontal, 0, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && (groundCheck || waterCheck ))
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            waterCheck = true;
        }
    }

}
