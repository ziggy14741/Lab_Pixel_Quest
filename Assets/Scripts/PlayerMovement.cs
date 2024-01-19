using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    // =================================================================================================================
    // VARIABLES 
    // =================================================================================================================

    private PlayerStats _playerStats;            // Used to keep track of player lives and ability to move 
    public float speed = 4;                      // Speed of the player 
    private Rigidbody2D _rigidbody2D;            // Rigidbody that controls player's physics 
    private SpriteRenderer _spriteRenderer;      // Used to flip the 2D image 
    private Animator _animator;                  // Animator that controls player animation states 

    // =================================================================================================================
    // METHODS  
    // =================================================================================================================

    private void Start(){
        _playerStats = GetComponent<PlayerStats>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (GetComponent<Animator>() != null) { _animator = GetComponent<Animator>();}
    }

    // Update is called once per frame
    private void Update(){
        // Checks if the player is able to move, if so check for player input 
        if(_playerStats.lives > 0 && !_playerStats.immobile){

            // Checks for player input A/Left Arrow to -1, D/Right Arrow for +1 and no keypress for 0 
            float xVelocity = Input.GetAxis("Horizontal");

            // If the player is moving left the sprite is flipped to face left, and vice versa 
            _spriteRenderer.flipX = xVelocity >= 0;

            // The xVelocity of the player is passed onto the rigidbody
            _rigidbody2D.velocity = new Vector2(speed * xVelocity, _rigidbody2D.velocity.y);
        }

        // If the animator has been attached switched states based on player input 
        if (_animator != null) {
            _animator.SetBool($"isMoving", _rigidbody2D.velocity.x != 0);
        }
    }
}

