using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHat : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 0.25f;
    public float jumpvelocity = 10f;
    private SpriteRenderer spriteRenderer;
    public Sprite explode;
    private Transform player;
    
   
    private void Start() {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        Destroy(gameObject, 6f);
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {   
            if (other.transform.DotTest(transform, Vector2.down))
            {
                Destroy(gameObject);
            }
            
            
        }
        
    }

    private void FixedUpdate() 
    {
        Vector2 target = new Vector2(player.position.x, player.position.y);
        Vector2 newpos = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newpos);
        
    }



    

}
