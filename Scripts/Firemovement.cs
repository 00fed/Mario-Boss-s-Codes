using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firemovement : MonoBehaviour
{   
    public Rigidbody2D rb;
    public float speed = 24f;
    public float jumpvelocity = 10f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public bool ground { get; private set; }
    public bool jump { get; private set; }
    public AudioSource fireball;
    public Vector2 direction = Vector2.right;
    public Vector2 directiony = Vector2.up;
    public Vector2 velocity;
    public Sprite explode;
    
   
    private void Start() {
        
        StartCoroutine(Explode());
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Block"))
        {   
            if (other.transform.DotTest(transform, Vector2.left) || other.transform.DotTest(transform, Vector2.right));
            animator.Play("Explode");
            gameObject.tag = "move";
            Destroy(gameObject, 0.1f);
        }
        if (other.gameObject.CompareTag("Luigi") || other.gameObject.CompareTag("Enemy"))
        {
            animator.Play("Explode");
            gameObject.tag = "move";
            Destroy(gameObject, 0.1f);
        }

    }

    private void FixedUpdate() 
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        ground = rb.Raycast(Vector2.up);
        
        if (ground)
        {   
            velocity.y = Mathf.Max(velocity.y, 0f);
            jump = velocity.y > 0f;
           velocity.y = directiony.y * jumpvelocity;
           jump = true;
        }
        
    }

    public IEnumerator Explode() {
        
        yield return new WaitForSeconds(1.9f);
        animator.Play("Explode");
        Destroy(gameObject, 0.1f);
        
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

   