using System.Collections;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shell;
    public float shellSpeed = 12f;
    public bool shellMoving;
    public AudioSource hit;
    public AudioSource shelling;
    private bool shelled;
    

     private void OnCollisionEnter2D(Collision2D collision)
     {
        if (!shelled && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                Shell();}
                else 
                {
                    player.Hit();
                }

            }

        
           if (collision.gameObject.layer == LayerMask.NameToLayer("Fireball")) {
            Hit();
             }
        
         
        
        }
     

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player"))
        {   
            
            if (!shellMoving)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);   
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.starpower) {
                    Hit();
                } else {
                    player.Hit();
                }
            }
            
        }

        
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Death Barrier")) 
        {
            Destroy(gameObject);
        }
            if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
    }
    public void Shell()
    {
       shelled = true;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shell;
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        shelling.Play();
        GameManager.Instance.Hundred();
        

         
    }
    
    private void PushShell(Vector2 direction)
    {
        shellMoving = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        EnemyMovement movement = GetComponent<EnemyMovement>();
        movement.speed = shellSpeed;
        movement.enabled = true;
        movement.direction = direction.normalized;
        hit.Play();
        gameObject.layer = LayerMask.NameToLayer("Shell");
        GameManager.Instance.FourHundred();
        
    }

    public void Hit()
    {   

        GetComponent<DeathAnimation>().enabled = true;
        hit.Play();
        Destroy(gameObject, 3f);
        GameManager.Instance.TwoHundred();
    }

    private void OnBecameInvisible()
    {
        if (shelled) {
            Destroy(gameObject);
        }
    }

  
}
