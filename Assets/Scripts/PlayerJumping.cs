using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    private PlayerStats _playerStats;

    public float jumpForce = 7;
    public float fallMultipler = 2;

    private Rigidbody2D _rigidbody2D;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Vector2 vecGravity;

    float height = 1f;
    float radius = 0.13f;

    // Start is called before the first frame update
    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerStats.lives > 0 && !_playerStats.immoblie)
        {
            isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(height, radius), CapsuleDirection2D.Horizontal, 0, groundLayer);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.y, jumpForce);
            }


            if (_rigidbody2D.velocity.y < 0)
            {
                _rigidbody2D.velocity -= vecGravity * fallMultipler * Time.deltaTime;
            }
        }
    }

}
