using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockCoin : MonoBehaviour
{   
    public Text score;
    
    void Start()
    {
        StartCoroutine(Animate());
        Destroy(gameObject, 0.8f);
        GameManager.Instance.AddCoin();

    }

  private IEnumerator Animate()
    {   
        Rigidbody2D rdb = GetComponent<Rigidbody2D>();
        CircleCollider2D physicsCollider = GetComponent<CircleCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Animator animator = GetComponent<Animator>();

        rdb.isKinematic = true;
        physicsCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.1f;

        float jumpVelocity = 25f;
        float gravity = 10f;

        Vector3 velocity = Vector3.up * jumpVelocity;
        Vector3 bottomEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
            animator.enabled = true;
        }

        rdb.isKinematic = false;
        physicsCollider.enabled = true;

        
    }

}
