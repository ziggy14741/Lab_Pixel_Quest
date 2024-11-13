using UnityEngine;
public class PQPlayerAnimator : MonoBehaviour
{
    // Speed 
    private Rigidbody2D rigidbody2D; // Checks player speed 

    // Animation  
    private Animator animator;     // Controls the animations 
    private string animParam = "isWalking"; // Param we are using to move between animation clips 

    // Start is called before the first frame update
    void Start()
    {
        // Connects components 
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if player is standing still if so make play Idle animation 
        if(rigidbody2D.velocity.x == 0)
        {
            animator.SetBool(animParam, false);
        }
        // Otherwise if player is moving play the walking animation 
        else
        {
            animator.SetBool(animParam, true);
        }
    }
}
