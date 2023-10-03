using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // unity variables
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sr;
    private Animator anim;
    


    // user difined variables
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private LayerMask jumpbleGround;
    private bool Fight;
    private float dirX = 0f;
    

    private enum MovementState { idle, run, combat_idle, jump, attack, hurt, recover, death }

    


    // Start is called before the first frame update
    private void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame

    private void Update()
    {
        

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded())
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        

        UpdateAnimationState();


    }


    private void UpdateAnimationState() // function for holding animations


    {
        MovementState state;
        if (dirX > 0)
        {
            state = MovementState.run;
            sr.flipX = true; // flip spite
           


        }
        else if (dirX < 0)
        {
            state = MovementState.run;
            sr.flipX=false;
           
        }

        else
        {
            state = MovementState.idle;
        }


        if (rb.velocity.y > .1f)
        {
            state = MovementState.jump;

        }

        
        
        anim.SetInteger("state", (int)state);
    }

    private bool isAttaked()
    {
        return true;
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpbleGround);
    }




    // function created for filp the sprite but it doesn't work correctly
    void Flip()
    {
        Vector3 scale = transform.localScale;
        if (dirX < 0)
        {
            transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
        }
        else
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
       
        
    }
}
