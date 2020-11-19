using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private bool isFlyingUp = true;
    private float flyingSpeed = 1.5f;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ground")
        {
            isFlyingUp = true;
        }
    }
}
