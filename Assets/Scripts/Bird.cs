using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool isFlyingVertical;
    public bool isFlyingHorizontal;
    public bool isFlyingSquare;

    private bool isFlyingUp = true;
    private bool isFlyingLeft = false;
    private bool isDead = false;
    private float flyingSpeed = 1.5f;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRender;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRender = GetComponent<SpriteRenderer>();
        if(isFlyingHorizontal)
        {
            _spriteRender.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            if (isFlyingVertical)
            {
                flyVertical();
            }

            if (isFlyingHorizontal)
            {
                flyHorizontal();
            }
        }
    }

    public void flyVertical()
    {
        if (isFlyingUp)
        {
            _rb.velocity = new Vector2(0f, flyingSpeed);
        }
        else
        {
            _rb.velocity = new Vector2(0f, -flyingSpeed);
        }
    }

    public void flyHorizontal()
    {
        if (isFlyingLeft)
        {
            _rb.velocity = new Vector2(-flyingSpeed,0f);
        }
        else
        {
            _rb.velocity = new Vector2(flyingSpeed,0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string name = collision.transform.name;
        if (name == "Boundary") 
        {
            if(isFlyingVertical)
            {
                if (isFlyingUp == true)
                {
                    isFlyingUp = false;
                }
                else
                {
                    isFlyingUp = true;
                }
            }
            else if(isFlyingHorizontal)
            {
                if (isFlyingLeft == true)
                {
                    isFlyingLeft = false;
                    _spriteRender.flipX = true;
                }
                else
                {
                    isFlyingLeft = true;
                    _spriteRender.flipX = false;
                }
            }
        }
    }

    public void Die()
    {
        _rb.gravityScale = 0f;
        _rb.velocity = new Vector2(0f, 0f);
        isDead = true;
        StartCoroutine(playDeathAnimation(.1f));
    }

    IEnumerator playDeathAnimation(float timeDeath)
    {
        // set death layer's weight to 1 to start death animation 
        _animator.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(timeDeath);
        // set death layer's weight to 0 to end death animation
        _animator.SetLayerWeight(1, 0);
        Destroy(this.gameObject);
    }
}
