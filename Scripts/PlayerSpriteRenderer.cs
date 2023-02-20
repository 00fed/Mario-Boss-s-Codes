using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
   public SpriteRenderer spriteRenderer { get; private set; }
   private PlayerMovement movement;
   private Player player;
   public FlagPole flagPole;
   public Sprite run;
   public Sprite slide;
   public Sprite idle;
   public Sprite jump;

   
   private void Awake() 
   {
    movement = GetComponentInParent<PlayerMovement>();
    player = GetComponentInParent<Player>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    

   }

private void LateUpdate() 
{  
    if (movement.jumping)
    {
        spriteRenderer.sprite = jump;
    }
    else if (movement.sliding)
    {
        spriteRenderer.sprite = slide; 
    }
    else if (movement.running)
    {
        spriteRenderer.sprite = run;
    }
    else if (flagPole.anim)
    {
        spriteRenderer.sprite = run;
    }
    else { 
        spriteRenderer.sprite = idle;
    }

    
}

}
