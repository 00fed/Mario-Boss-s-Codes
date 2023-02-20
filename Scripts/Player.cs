using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    public PlayerSpriteRenderer fireRenderer;
    public SpriteRenderer fireR;
    public SpriteRenderer smallR;
    public SpriteRenderer bigR;
    
    
    public Fire fire;
    public Fireball fireball;
    private PlayerSpriteRenderer activeRenderer;
    private SpriteRenderer activeR;
    
    
    [SerializeField] private AudioSource death;
    public AudioSource powerup;
    public AudioSource shrink;
    
    public CapsuleCollider2D capsuleCollider {get; private set; }
    public DeathAnimation deathRenderer { get; private set; }
    public bool big => bigRenderer.enabled || fireRenderer.enabled;
    public bool small;
    public bool dead => deathRenderer.enabled;
    public bool starpower { get; private set; }

    

    private void Awake()
    {   capsuleCollider = GetComponent<CapsuleCollider2D>();
        deathRenderer = GetComponent<DeathAnimation>();
        activeRenderer = smallRenderer;
        activeR = smallR;
        fireball.enabled = false;
        
        
    }
    
    
    public void Hit()
    {  if (!dead && !starpower)
       {
        if(big)
        {
            Shrink();
            
        }
        else {
          Death();
        }
       }

    }

    public void Death()
    {
      smallRenderer.enabled = false;
      bigRenderer.enabled = false;
      deathRenderer.enabled = true;
      
      
      
      
      GameManager.Instance.ResetLevel(3f);
      
      death.Play(); 
      
        
    }
    
    public void Fire()
    {
        smallRenderer.enabled = false;
      smallR.enabled = false;
      bigR.enabled = false;
      bigRenderer.enabled = false;
      fireR.enabled = true;
      fireRenderer.enabled = true;
      activeRenderer = fireRenderer;
      activeR = fireR;
      fireball.enabled = true;

      capsuleCollider.size = new Vector2(0.74f, 2f);
      capsuleCollider.offset = new Vector2(0f, 0.5f);
      
      powerup.Play();
      StartCoroutine(FireAnimation());
    }

    public void Grow()
    {
      smallRenderer.enabled = false;
      smallR.enabled = false;
      bigR.enabled = true;
      bigRenderer.enabled = true;
      activeRenderer = bigRenderer;
      activeR = bigR;
      fireball.enabled = false;
      capsuleCollider.size = new Vector2(0.74f, 2f);
      capsuleCollider.offset = new Vector2(0f, 0.5f);
      
      fire.Change();
      if (small)
      {
        fire.ReChange();
      }
      powerup.Play();
       
      StartCoroutine(ScaleAnimation());
    }

    public void Shrink()
    {
      small = true;
      smallRenderer.enabled = true;
      smallR.enabled = true;
      bigR.enabled = false;
      bigRenderer.enabled = false;
      activeRenderer = smallRenderer;
      activeR = smallR;
      fireball.enabled = false;
      fire.Changesmall();
      
      
      capsuleCollider.size = new Vector2(0.75f, 1f);
      capsuleCollider.offset = new Vector2(0f, 0f);

      shrink.Play();

      StartCoroutine(ScaleAnimation());
    }
    
    private IEnumerator FireAnimation()
    {
        float elapsed = 0f;
        float duration = 1.25f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

         activeRenderer.spriteRenderer.color = Color.white;
    }
    private IEnumerator ScaleAnimation()
    {  
        
        float elapsed = 0f;
        float duration = 1.25f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (Time.frameCount % 10 == 0)
            {   
                
                smallRenderer.enabled = !smallRenderer.enabled;
                bigRenderer.enabled = !bigRenderer.enabled;
                fireRenderer.enabled = false;
                fireR.enabled = false; 
                smallR.enabled = !smallR.enabled;
                bigR.enabled = !bigR.enabled;
                gameObject.layer = LayerMask.NameToLayer("Shelby");

            }
            yield return null;
            
            
        }
       
        
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        fireRenderer.enabled = false;
        bigR.enabled = false;
        smallR.enabled = false;
        fireR.enabled = false;
        activeR.enabled = true;
        activeRenderer.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    

    public void Starpower()
    {
        StartCoroutine(StarpowerAnimation());
        powerup.Play();
    }

    private IEnumerator StarpowerAnimation()
    {
        starpower = true;

        float elapsed = 0f;
        float duration = 12f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) {
                activeRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeRenderer.spriteRenderer.color = Color.white;
        starpower = false;
     
    }

    private void OnTriggerEnter2D(Collider2D other)

    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Death Barrier")) 
        {   
            if (big)
            {
                bigRenderer.enabled = false;
                fireRenderer.enabled = false;
                fireR.enabled = false;
                bigR.enabled = false;
                smallRenderer.enabled = true;
                smallR.enabled = true;
                Death();
            }
            else {
                Death();
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hat"))
        {
          Hit();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("BigHat"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                return;
            }
            else
            {
                Hit();
            }
        }
        if (collision.gameObject.CompareTag("Luigi"))
        {
            if(collision.transform.DotTest(transform, Vector2.up))
            {
                return;
            }
            else {
                Hit();
            }
        }
    }

    
    
}
   


