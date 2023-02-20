using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject mushroom;
    public GameObject fire;
    public GameObject mushroom2;
    public GameObject fire2;
    public Player player;
    
    private void Awake() {
        mushroom.SetActive(true);
        fire.SetActive(false);
        mushroom2.SetActive(false);
        fire2.SetActive(false);
    }

    public void Change() {
        
            mushroom.SetActive(false);
            fire.SetActive(true);
            mushroom2.SetActive(false);
            fire2.SetActive(false);


    }

    public void Changesmall()
    {
        mushroom.SetActive(false);
        fire.SetActive(false);
        mushroom2.SetActive(true);
        fire2.SetActive(false);

    }
    public void ReChange()
    {
        mushroom.SetActive(false);
        fire.SetActive(false);
        mushroom2.SetActive(false);
        fire2.SetActive(true);
    }

}
