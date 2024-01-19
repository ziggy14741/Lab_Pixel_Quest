using UnityEngine;

public class PlayerJumping : MonoBehaviour {
    
    // =================================================================================================================
    // VARIABLES 
    // =================================================================================================================
   
    private PlayerStats _playerStats;           // Used to keep track of player lives and ability to move 
    private Rigidbody2D _rigidbody2D;           // Rigidbody that controls player's physics
    
    public float jumpForce = 7;                 // How high the player will jump
    public float fallMultiplier = 2;            // The speed increase that the player will fall down with 
    private Vector2 _gravityVector;             // The velocity which will modify the rigidbody 
    
    public Transform groundCheck;               // Transform around which the hit-box will be created around 
    public LayerMask groundLayer;               // Name of the layer which tells us the hit-box is interacting with the ground 
    private bool _isGrounded;                   // Tells us if the hit-box is hitting ground, if so player can jump 
    private const float Height = 1f;            // Height of the hit-box
    private const float Radius = 0.08f;         // Radius of the hit-box 

    // =================================================================================================================
    // METHODS  
    // =================================================================================================================

    // Start is called before the first frame update
    private void Start() {
        _gravityVector = new Vector2(0, -Physics2D.gravity.y);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    private void Update() {
        if (_playerStats.lives <= 0 || _playerStats.immobile) return;
        
        // Checks if the player is touching the ground 
        _isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(Height, Radius), CapsuleDirection2D.Horizontal, 0, groundLayer);

        // Gives the player upwards velocity 
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded) {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.y, jumpForce);
        }

        // Once the player starts falling accelerate the gravity to make the fall faster 
        if (_rigidbody2D.velocity.y < 0) {
            _rigidbody2D.velocity -= _gravityVector * (fallMultiplier * Time.deltaTime);
        }
    }

}
