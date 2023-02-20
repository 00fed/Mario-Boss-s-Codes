using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   private Camera cam;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    public float moveSpeed = 8f;
    private Vector2 velocity;
    private float inputAxis;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public AudioSource smalljump;
    public AudioSource bigjump;
    public Player player;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);
    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f &&  velocity.x < 0f) || (inputAxis < 0f &&  velocity.x > 0f);
    private Goomba goomba;
    public Firemovement firemovement;
    public Luigi luigi;
    
    
    
    private void Awake() 
    {
       rb = GetComponent<Rigidbody2D>(); 
       capsuleCollider = GetComponent<CapsuleCollider2D>();
       cam = Camera.main;
       goomba = GetComponent<Goomba>();
    }

    private void Update() 
    { 
      
      HorizontalMovement();
      grounded = rb.Raycast(Vector2.down);
      if (grounded) 
      {
        
        GroundedMovement();
        
        
      }   
      
      ApplyGravity();
    }
   
   public void HorizontalMovement() 
   {
     inputAxis = Input.GetAxis("Horizontal");
     velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
     if (Input.GetKey("z"))
     {
       moveSpeed = 12;
     }
     else {
      moveSpeed = 8;
     }
     
     if (rb.Raycast(Vector2.right * velocity.x)) 
     {
        velocity.x = 0f;
     }
     if (velocity.x > 0f) {
            transform.eulerAngles = Vector3.zero;
            firemovement.Slippe();
        } else if (velocity.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            firemovement.Slip();
        }
   }
   

   private void GroundedMovement()
   {
    velocity.y = Mathf.Max(velocity.y, 0f);
    jumping = velocity.y > 0f;
    if (Input.GetButtonDown("Jump"))
    {
      velocity.y = jumpForce;
      jumping = true;
      if (player.big)
      {
        bigjump.Play();
      }
      else {smalljump.Play();}
    }
     
   }

    private void ApplyGravity() 
   {
    
    bool falling = velocity.y < 0f || !Input.GetButton("Jump");
    float multiplier = falling ? 2f : 1f;
    velocity.y += gravity * multiplier * Time.deltaTime;
    velocity.y = Mathf.Max(velocity.y, gravity / 2f);
   }

   private void FixedUpdate() 
   {
    Vector2 position = rb.position;
    position += velocity * Time.fixedDeltaTime;

    Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
    Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

    
    

    
    rb.MovePosition(position);
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
    if(collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
    {
      if (transform.DotTest(collision.transform, Vector2.up))
      {
        velocity.y = 0f;
      }
      
    }

    if (collision.gameObject.CompareTag("Enemy"))
      { 
        if((collision.transform.DotTest(transform, Vector2.up)))
      {

        velocity.y = jumpForce / 1.5f;
        jumping = true;
        
      }
    
      
      
      }
      if (collision.gameObject.CompareTag("move"))
    {
      if((collision.transform.DotTest(transform, Vector2.up)))
      {

        velocity.y = jumpForce / 1.5f;
        jumping = true;
        
      }
    }
      if (collision.gameObject.CompareTag("Luigi"))
        {
            if(collision.transform.DotTest(transform, Vector2.up))
            {
                velocity.y = jumpForce * 1.5f;
                jumping = true;
                if (luigi.boss)
            {
              if (velocity.x > 0)
              {
                velocity.x = velocity.x + 10f; 
              }
              else if (velocity.x < 0)
              {
                velocity.x = velocity.x - 10f; 
              }
            }
              else if (!luigi.boss)
              {
                velocity.x = velocity.x - 10f; 
              }
                
            }   

            
          
        }
   }



}





