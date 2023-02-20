using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

   public GameObject item;
   public Animator animator;
   public KeyCode enterKeyCode;
   public KeyCode keyCode;
   public AudioSource fireball;
   public Transform item3;
   public int maxFire = 2;
   public SpriteRenderer s;
   
    void Update()
    {
       StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {  
        
        if(Input.GetKeyDown(enterKeyCode))
       {
        maxFire--;
        fireball.Play();
        if (item != null)
        {
            Instantiate(item, item3.position, item3.rotation);
        }
        if (maxFire == 0)
        {enterKeyCode = KeyCode.None;
        yield return new WaitForSeconds(1f);
        maxFire = 2;
        enterKeyCode = KeyCode.LeftControl;
        }
       }
      

    }
}
