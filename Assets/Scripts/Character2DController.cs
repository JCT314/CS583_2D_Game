using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Character2DController : MonoBehaviour
{
    private float movementSpeed = 5f;
    private float jumpForce = 5f;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRender;
    private Transform background;
    private Transform mainCamera;
    private PlayerManager playerManager;
    private TextMeshProUGUI lives_info_text;
    private bool isHit = false;

    bool isGrounded;

    
    Transform groundCheck;
    Transform groundCheckL;
    Transform groundCheckR;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRender = GetComponent<SpriteRenderer>();
        background = GameObject.FindGameObjectWithTag("Background").GetComponent<Transform>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        groundCheck = GameObject.Find("GroundCheck").GetComponent<Transform>();
        groundCheckL = GameObject.Find("GroundCheckL").GetComponent<Transform>();
        groundCheckR = GameObject.Find("GroundCheckR").GetComponent<Transform>();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        lives_info_text = GameObject.Find("Text_TMP_Lives_Count").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // have the camera and background follow the player
        background.position = new Vector3(transform.position.x, 0f, 0f);
        mainCamera.position = new Vector3(transform.position.x, 0f, -10f);
        lives_info_text.text = "X " + playerManager.player.getLives().ToString();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Finish")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("SampleScene");
        } 
        else if(collision.transform.tag == "Trap" || collision.transform.tag == "Enemy" || collision.transform.name == "Air" && !isHit)
        {
            loseLife();
            isHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Trap" || collision.transform.tag == "Enemy" || collision.transform.name == "Air" && !isHit)
        {
            isHit = false;
        }
    }
    private void loseLife()
    {
        // move back to the beginning
        _rb.transform.position = new Vector3(-4f, -2f, 0f);
        playerManager.player.subtractLife();
        // start playing hurt animation
        StartCoroutine(playHurtAnimation(.6f));
    }

    IEnumerator playHurtAnimation(float timeHurt)
    {
        // set hit layer's weight to 1 to start hurt animation 
        _animator.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(timeHurt);
        // set hit layer's weight to 0 to endy hurt animation
        _animator.SetLayerWeight(1, 0);
    }
}
