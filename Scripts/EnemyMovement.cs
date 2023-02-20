using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour

{
    public float speed = 1f;
 public Vector2 direction = Vector2.left;
 private new Rigidbody2D  rdb;
 private Vector2 velocity;

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
        Slip();

        if (rdb.Raycast(Vector2.down)) {
            velocity.y = Mathf.Max(velocity.y, 0f);
        }
        
       
    }
        
    private void OnTriggerEnter2D(Collider2D other)
    {   
       
          if (gameObject.layer == LayerMask.NameToLayer("Enemy")){
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
      
            direction = -direction;
                
            
        
        }
          }
    }
    private void Slip()
    {
     if (velocity.x < 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
     else if (velocity.x > 0f )
     {
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
     }
    }
    
}

   
    
   
   

    

