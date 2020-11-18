using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    private float movementSpeed = 1.5f;
    private float jumpForce = 3f;
    private Rigidbody2D _rb;
    private  Animator _animator;
    private SpriteRenderer _spriteRender;

    bool isGrounded;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /* 
            Checks if either of the 3 ground checks when casting a line between the player transform
            and the ground checks hits a gameobject that is in the Ground layer. If so then the player
            is grounded. Used to later check if player can jump.
        */
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            _animator.Play("Player_Jump");
        }

        if(Input.GetKey("d"))
        {
            _rb.velocity = new Vector2(movementSpeed, _rb.velocity.y);
            if(isGrounded)
            {
                _animator.Play("Player_Run");
            }
            // since player is moving right then sprite does not need to be flipped
            _spriteRender.flipX = false;
        }
        else if(Input.GetKey("a"))
        {
            _rb.velocity = new Vector2(movementSpeed * -1, _rb.velocity.y);
            if(isGrounded)
            {
                _animator.Play("Player_Run");
            }
            // since player is moving left then sprite does  need to be flipped
            _spriteRender.flipX = true;
        }
        else
        {
            if(isGrounded)
            {
                _animator.Play("Player_Idle");
            }
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
        }

        // checks to see if you can jump
        if (Input.GetKey("space") && isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _animator.Play("Player_Jump");
        }
    }
}
