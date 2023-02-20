using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Goomba : MonoBehaviour
{    
    public Sprite flat;
    public AudioSource flatten;
    public AudioSource hit;
    
    
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                Flatten();
                
                
            }
             else 
                {
                    player.Hit();
            } 


            
        }
       if (collision.gameObject.CompareTag("Brick"))
       {
        
           if (collision.transform.DotTest(transform, Vector2.up)) {
            Hit();
            }
       }
        

        if (collision.gameObject.layer == LayerMask.NameToLayer("Fireball")) {
            Hit();
        }
        
        
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")) {
            Hit();
        }
        if (other.gameObject.CompareTag("Hat")) {
            Hit();
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Death Barrier")) 
        {
            Destroy(gameObject);
        }
    }

    public void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flat;
        flatten.Play();
        Destroy(gameObject, 0.5f);
        GameManager.Instance.Hundred();
    }

     public void Hit()
    {   
        GetComponent<DeathAnimation>().enabled = true;
        hit.Play();
        Destroy(gameObject, 3f);
        GameManager.Instance.Hundred();

    }

}
