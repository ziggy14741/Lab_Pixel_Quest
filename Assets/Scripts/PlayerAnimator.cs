using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private string animParam = "isWalking";

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rigidbody2D.velocity.x == 0)
        {
            animator.SetBool(animParam, false);
        }
        else
        {
            animator.SetBool(animParam, true);
        }
    }
}
