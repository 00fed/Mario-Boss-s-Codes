using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luigi : MonoBehaviour
{
    public int maxHealth = 1000;
    public int currentHealth;
    public float speed = 8f;
    public float startingrange = 5f;
    public bool stage;
    public bool boss = false;
    public Background under;
    public GameObject Black;
    private Rigidbody2D rb;
    public Animator a;
    public Animator death;
    public SpriteRenderer sp;
    public CapsuleCollider2D cc;
    public GameObject HealthBar;
    public GameObject MysteryMushroom;
    private Transform player;
    public LuigiHealthBar healthBar;
    public bool isVulnerable = false;
    void Start() {
     currentHealth = maxHealth;   
     healthBar.SetMaxHealth(maxHealth);
     rb = GetComponent<Rigidbody2D>();
     player = GameObject.FindWithTag("Player").transform;
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
      if (collision.gameObject.CompareTag("Player"))
      {
        
        if (collision.transform.DotTest(transform, Vector2.down))
        {
            TakeDamage(5);
           
          
        }
        
      }
      if (collision.gameObject.layer == LayerMask.NameToLayer("Fireball"))
        {
        TakeDamage(5);
        
        
        }  
    }
      
    
    
      
    

    void TakeDamage(int damage)
    {   

      if (isVulnerable)
      {
        return;
      }
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth == 0)
        {
          
          sp.enabled = false;
          a.SetBool("Third", false);
          a.enabled = false;
          death.enabled = true;
          SecondStage();
          Destroy(gameObject, 4f);
          

        }
        if (currentHealth <= 250)
        {
          boss = true;
          a.SetBool("First", false);
           a.ResetTrigger("Attack");
           a.SetBool("Eat", false);
           a.ResetTrigger("BigAttack");

           a.SetBool("Third", true);
          cc.size = new Vector2(5.4f, 10f);
           cc.offset = new Vector2(0f, 0.5f);
           MysteryMushroom.SetActive(true);
        }
        if (currentHealth <= 600 &&  currentHealth >= 251)
        {  
           //Vector2 target = new Vector2(exit.position.x, rb.position.y);
           //Vector2 newpos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
           //rb.MovePosition(newpos);

           a.SetBool("First", false);
           a.ResetTrigger("Attack");
           a.SetBool("Eat", true);
           a.SetTrigger("BigAttack");
           cc.size = new Vector2(0.74f, 2f);
           cc.offset = new Vector2(0f, 0.5f);
           
           
        }

        if (currentHealth <= 999 && currentHealth >= 601)
        {
          
          
          a.SetBool("First", true);
          stage = true;
          a.SetTrigger("Attack");
          Camera.main.GetComponent<SideScrolling>().SetBoss();
          Black.SetActive(true);
          HealthBar.SetActive(true);

          
        }

       
        
    }
    
     private void OnTriggerEnter2D(Collider2D collision) {
      if (collision.gameObject.CompareTag("Player"))
      {
        if (collision.transform.DotTest(transform, Vector2.up))
        {
           GameObject.FindWithTag("Player").GetComponent<Player>().Hit();
        }
        
      }
      
     }
    
      
     //StartCoroutine(SecondStage(gameObject.transform));
    
    private void SecondStage()
    {
      
      

      Camera.main.GetComponent<SideScrolling>().ResetBoss();
      Black.SetActive(false);    
      HealthBar.SetActive(false);
      stage = false;
      

      //yield return MoveToPosition(luigi, start.position);
      //yield return MoveToPosition(luigi, luigi.position + Vector3.left);
      //yield return MoveToPosition(luigi, exit.position);
      //Vector3 luigip = luigi.position;
      //luigip.x = 102.7f;
      //luigi.position = luigip;
      


    }


    private IEnumerator MoveToPosition(Transform transform, Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.125f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        
            
            yield return null;
        }
        
        transform.position = position;
    }
    
}

