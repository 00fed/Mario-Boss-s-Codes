using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipeconnection : MonoBehaviour
{   
    public Transform player;
    public GameObject blackscreen;
    public Transform playera;
    public Transform playerb;
    public Transform playertransform;
    public float speed = 6f;

    public void Animet()
    {
        StartCoroutine(Animate());
        
    }

    private IEnumerator Animate()
    {   
        Rigidbody2D rdb = GetComponent<Rigidbody2D>();
        CapsuleCollider2D physicsCollider = GetComponent<CapsuleCollider2D>();
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        spriteRenderer.sortingOrder = 0;
              

        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.down;
        

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;
            
            yield return null;
        }
       
       blackscreen.SetActive(true);
       StartCoroutine(ChangingPosition());
       Camera.main.GetComponent<SideScrolling>().SetUnderground();
       yield return new WaitForSeconds(0.75f);
       spriteRenderer.sortingOrder = 4;
       blackscreen.SetActive(false);
       
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


    private IEnumerator ChangingPosition()
    { 

      Rigidbody2D rdb = GetComponent<Rigidbody2D>();

        yield return MoveToPosition(player, playera.position);
        yield return MoveToPosition(player, player.position + Vector3.down);
        yield return MoveToPosition(player, playerb.position);
        yield return MoveToPosition(player, player.position + Vector3.right);
        yield return MoveToPosition(player, playertransform.position);


      
    }
}
