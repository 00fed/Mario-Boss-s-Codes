using UnityEngine;




public class Block : MonoBehaviour
{
    
    public GameObject item;
    public Transform item3;
    public Transform item4;
    public AudioSource sound;
    public int maxHits = -1;
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (maxHits < 0)

        {
            if (collision.transform.DotTest(transform, Vector2.up))
           { Player player = collision.gameObject.GetComponent<Player>(); 
             if (player.big)
                {   
                    GameManager.Instance.Fifty();
                    Hit4();
                    Hit3();
                    Destroy(gameObject, 0.1f);
                    
                    
                    

                } 
           }
        }
    }
    
    private void Hit4()
    {
      

        if (item != null) {
            
            Instantiate(item, item4.position, item4.rotation);
        }

        
    }
    private void Hit3()
    {   
        maxHits--;

        if (item != null) {
            
            Instantiate(item, item3.position, item3.rotation);
        }

        
        
    }

}
