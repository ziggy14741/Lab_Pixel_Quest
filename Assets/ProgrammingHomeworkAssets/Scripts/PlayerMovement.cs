using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Components 
    private Rigidbody2D _rigidbody2D;

    // Velocity 
    private float _xVelocity; 
    private float _yVelocity;
    public float speed = 3;

    // Input 
    private string HorizontalAxis = "Horizontal";
    private string VerticalAxis = "Vertical";

    // Gets the component connection 
    private void Start() {  _rigidbody2D = GetComponent<Rigidbody2D>();    }


    private void Update()
    {
        // Gets input from horizntal and vecticl inputs, -1 if left click, 0 if no click, 1 if right click 
        _xVelocity = Input.GetAxis(HorizontalAxis);
        _yVelocity = Input.GetAxis(VerticalAxis);

        // Push the input data to the rigidbody 
        _rigidbody2D.velocity = new Vector2(_xVelocity, _yVelocity) * speed;
    }

}
