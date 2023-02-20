using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 24f;
    public float jumpvelocity = 10f;
    private SpriteRenderer spriteRenderer;
    public Vector2 direction = Vector2.right;
    public Vector2 directiony = Vector2.up;
    private Transform player;
    public Vector2 velocity;
    public Sprite explode;
    
   
    private void Start() {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 24f);
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Luigi"))
        {   
            if (other.transform.DotTest(transform, Vector2.left) || other.transform.DotTest(transform, Vector2.right));
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate() 
    {
        velocity.x = direction.x * speed;
        velocity.y = velocity.y;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        if (rb.Raycast(Vector2.left)) {
            direction = Vector2.right;
        }
        
    }


    public void Slip()
    {
        direction = Vector2.left;
    }

    public void Slippe()
    {
        direction = Vector2.right;
    }
}
