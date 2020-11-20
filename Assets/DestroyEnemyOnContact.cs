using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyOnContact : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string hitTag = collision.transform.tag;
        Debug.Log(hitTag);
        if (hitTag == "Enemy")
        {
            Bird enemy = collision.gameObject.GetComponent<Bird>();
            enemy.Die();
        }
    }


}
