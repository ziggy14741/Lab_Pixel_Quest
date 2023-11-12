using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _playerStats;
    public float speed = 4;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (GetComponent<Animator>() != null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerStats.lives > 0 && !_playerStats.immoblie)
        {
            float xVelcoity = Input.GetAxis("Horizontal");
            if (xVelcoity < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            _rigidbody2D.velocity = new Vector2(speed * xVelcoity, _rigidbody2D.velocity.y);
        }

        if (_rigidbody2D.velocity.x == 0 && _animator != null)
        {
            _animator.SetBool("isMoving", false);
        }
        else
        {
            _animator.SetBool("isMoving", true);
        }
    }


}

