using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeconnection2 : MonoBehaviour
{
    public Transform connection;
    public GameObject black;
    public GameObject player;
    public Transform exit;
    public Vector3 enterDirection = Vector3.right;
    public Vector3 exitDirection = Vector3.zero;
    public AudioSource pipe;
    public GameObject trigger;
    public float speed = 8;
    public bool animate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {     
                 
                   pipe.Play();
                    StartCoroutine(Enter(collision.transform));
                   
                }
        }
    

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        
        animate = true;
        trigger.tag = ("Over");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return MoveToPosition(player.transform, exit.position);
        black.SetActive(true);
        yield return new WaitForSeconds(1f);
        black.SetActive(false);

        Camera.main.GetComponent<SideScrolling>().SetGround();


        player.position = connection.position;
        player.localScale = Vector3.one;
        player.GetComponent<PlayerMovement>().enabled = true;
    }
     
    private IEnumerator MoveToPosition(Transform transform, Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.125f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            
            yield return null;
        }
        
        transform.position = position;
    }

}
