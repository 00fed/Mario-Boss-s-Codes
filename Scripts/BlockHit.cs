using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    
    public GameObject item;
    public Sprite emptyBlock;
    public int maxHits = 1;
    public bool animating;
    public bool destroying;
    public AudioSource sound;
    public AudioSource bump;
    public Transform item1;
    public Transform item2;
    


    private void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if (!animating && maxHits > 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up)) {
                
                Hit();
                sound.Play();
                StartCoroutine(ChangeTag());
                }
        }
        if (maxHits == 0 && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up)) {
                bump.Play();
                }
        }
        if (maxHits < 0)

        {
            if (collision.transform.DotTest(transform, Vector2.up))
           { Player player = collision.gameObject.GetComponent<Player>(); 
             if (player.big)
                {   
                    Hit2();
                    Hit1();
                    
                    StartCoroutine(ChangeTag());
                    Destroy(gameObject, 0.1f);
                    
                    

                } 
            else {
                Hitt();
                sound.Play();
                StartCoroutine(ChangeTag());
                
            }
           }
        }
    }


    public IEnumerator ChangeTag()
    {
        gameObject.tag = ("Brick");
        yield return new WaitForSeconds(1f);
          gameObject.tag = ("Default");
    }

    
    private void Hit2()
    {
        if (item != null) {
            
            Instantiate(item, item2.position, item2.rotation);
            
        }

        
        
    
    }
    private void Hit1()
    {   
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        maxHits--;

        if (item != null) {
            
            Instantiate(item, item1.position, item1.rotation);
            
        }
          
        StartCoroutine(Animate());
    }

    private void Hitt()
    {
        maxHits--;

        StartCoroutine(Animate());
    }
    private void Hit()
    {   
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Animator>().enabled = false;
        spriteRenderer.enabled = true; // show if hidden

        maxHits--;

        if (maxHits == 0) {
            spriteRenderer.sprite = emptyBlock;
        }
        

        if (item != null) {
                   Instantiate(item, transform.position, Quaternion.identity);
                   }
        

        
        
        
        StartCoroutine(Animate());
        
    }

    private IEnumerator Animate()
    {
        animating = true;
        

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        
        animating = false;
        
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {   
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
        

    }

    

    
}
