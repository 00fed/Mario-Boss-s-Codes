using System.Collections;
using UnityEditor;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
       public float speed = 3f;
    public Vector2 direction = Vector2.right;
    private new Rigidbody2D  rdb;
    public Vector2 directiony = Vector2.up;
    public bool ground { get; private set; }
    public bool jump { get; private set; }
    public float jumpvelocity = 10f;
    private Vector2 velocity;
    public Sprite star;



    
        private void Start()
    {
        StartCoroutine(Animate());
        
        

    }

        private void Awake()
    {
        rdb = GetComponent<Rigidbody2D>();
        enabled = false;
    }

   private void OnBecameVisible()
    {
        #if UNITY_EDITOR
        enabled = !EditorApplication.isPaused;
        #else
        enabled = true;
        #endif
    }

   private void OnBecameInvisible()
    {
        enabled = false;
    }
   private void OnEnable()
    {
        rdb.WakeUp();
    }
    private void OnDisable()
    {
        rdb.velocity = Vector2.zero;
        rdb.Sleep();
    }

    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        rdb.MovePosition(rdb.position + velocity * Time.fixedDeltaTime);

        if (rdb.Raycast(direction)) {
            direction = -direction;
        }
        ground = rdb.Raycast(Vector2.down);
        
        if (ground) {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
        if (ground && star != null)
        {   
            
            jump = velocity.y > 0f;
           velocity.y = directiony.y * jumpvelocity;
           jump = true;
        }
       
    }


    private IEnumerator Animate()
    {
        
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rdb.isKinematic = true;
        physicsCollider.enabled = false;
        triggerCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.up;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;
            
            yield return null;
        }

        rdb.isKinematic = false;
        physicsCollider.enabled = true;
        triggerCollider.enabled = true;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {   
       
          
            if (other.gameObject.layer == LayerMask.NameToLayer("Coin")) {
      
            direction = -direction;
                
            
        
        }
          
    }




}
