using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Player player;
    public Luigi luigi;
    public AudioSource overground;
    public GameObject nine;
    public Transform nin;
    public Transform pl;
    public AudioClip speedoverground;
    public AudioSource underground;
    public AudioSource star;
    public AudioSource up;
    public AudioSource coin;
    public AudioClip speedunderground;
    public AudioSource boss;
    

    
    
    private void FixedUpdate() {
      if (GameManager.Instance.tpme)
       {
         
         nine.GetComponent<BoxCollider2D>().enabled = true;
         nine.tag = ("Time");
       }
       if (GameManager.Instance.tame)
       {
         nine.GetComponent<BoxCollider2D>().enabled = false;
        
       }
       nin.position = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Flag"))
        {
          
            overground.Stop();
          
        }
        if (other.gameObject.CompareTag("Star"))
        {
            StartCoroutine(s());
        }
        if (other.gameObject.CompareTag("Up"))
        {
            up.Play();
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            coin.Play();
        }
        if (other.gameObject.CompareTag("Under"))
          { 
          overground.Stop();
          underground.Play();
          
          }
        if (other.gameObject.CompareTag("Over"))
         {  
            player = GetComponent<Player>();
            overground.Play();
            underground.Stop();
            
            
         }
         if (other.gameObject.CompareTag("move"))
         {
          
            overground.Stop();
            underground.Stop();
         }
         if (Camera.main.GetComponent<SideScrolling>().ground)
          {
            if (other.gameObject.CompareTag("Time"))
           {
             underground.clip = speedunderground;
             overground.clip = speedoverground;
             overground.Play();

           }
         }

         if (Camera.main.GetComponent<SideScrolling>().underground)
          {
            if (other.gameObject.CompareTag("Time"))
           {
             underground.clip = speedunderground;
             overground.clip = speedoverground;
             underground.Play();
             overground.Stop();

           }
         }
         

          if (other.gameObject.layer == LayerMask.NameToLayer("Death Barrier"))
          {
            overground.Stop();
            underground.Stop();
            }
          
          if (other.gameObject.CompareTag("GameOver"))
         {
            overground.Stop();
            underground.Stop();
         }

        } 

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Under"))
          { 
           other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
           
          }
        if (other.gameObject.CompareTag("Over"))
         {
           other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
           
         }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Luigi"))
          { if (!player.small)
            {
            if (collision.transform.DotTest(transform, Vector2.up)) {
            
             }
            else {overground.Stop();
            underground.Stop();}
            }
          }
        //if (luigi.boss)
        //{
          //boss.Play();
          //overground.Stop();
          //underground.Stop();
       // }
       // else {
        //  return;
       // }
    }

    public IEnumerator s()
    {  
       overground.Stop();
       star.Play();

       yield return new WaitForSeconds(12f);

       overground.Play();
       


    }




}
