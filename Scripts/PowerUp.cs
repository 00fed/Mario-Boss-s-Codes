using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{    
    
    public enum Type
    {   

        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
        Fire,
    }

    public Type type;
    public AudioSource coin;
    public AudioSource starpower;
    public AudioSource up;
    public GameObject invisible;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {   
            case Type.Coin:
                GameManager.Instance.AddCoin();
                if (coin !=null)
                {coin.Play();}
                break;
                

            case Type.ExtraLife:
                GameManager.Instance.AddLife();
                if (up !=null)
                {up.Play();}
                Destroy(invisible, 15f);
                GameManager.Instance.Thousand();
                break;
                


            case Type.MagicMushroom:
                player.GetComponent<Player>().Grow();
                GameManager.Instance.Thousand();
                break;
                

            case Type.Starpower:
                player.GetComponent<Player>().Starpower();
                if (starpower !=null)
                {starpower.Play();}
                GameManager.Instance.Thousand();
                break;
                

            case Type.Fire:
                player.GetComponent<Player>().Fire();
                GameManager.Instance.Thousand();
                break;
                
        }

        Destroy(gameObject);
    }
}
