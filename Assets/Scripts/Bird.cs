using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isFlyingUp = true;
    private bool isDead = false;
    private float flyingSpeed = 1.5f;
    private Rigidbody2D _rb;
    private RigidbodyConstraints2D _rbConstraints;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            if(isFlyingUp)
            {
                _rb.velocity = new Vector2(0f,flyingSpeed);
            }
            else
            {
                _rb.velocity = new Vector2(0f,-flyingSpeed);
            }

            if(_rb.transform.position.y > 0f)
            {
                isFlyingUp = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string hitTag = collision.transform.tag;
        string hitName = collision.transform.name;
        if (hitName == "Ground" || hitTag == "Boundary")
        {
            isFlyingUp = true;
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
