using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuigiFireball : MonoBehaviour
{
    public GameObject item;
    public GameObject item2;
   public KeyCode enterKeyCode;
   public Transform item3;
   
    void a()
    {
       
       
        
        if (item != null)
        {
            Instantiate(item, item3.position, item3.rotation);
        }
    
    }
    void b()
    {
        if (item2 != null)
        {
            Instantiate(item2, item3.position, item3.rotation);
        }
    }
}
