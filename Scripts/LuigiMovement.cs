using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LuigiMovement : MonoBehaviour
{       
        public Transform player; 
        private Transform transform;
        public Hat hat;
        
        private void Awake() {
            transform = GameObject.FindWithTag("Luigi").transform;
        }

        

        public void Slip()
        {
            if (transform.position.x > player.position.x) {
            transform.eulerAngles = new Vector3(0,180,0);
            hat.Slip();
        } else if (transform.position.x < player.position.x) {
            transform.eulerAngles = Vector3.zero;
            hat.Slippe();
        }
        }

        

 }
        
       

        

        

    

        
        
        
        
        
        
        
        
        
    


