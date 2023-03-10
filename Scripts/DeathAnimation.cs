using UnityEngine;
using System.Collections;

public class DeathAnimation : MonoBehaviour

   {
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;
    public Animator animator;
    

    
    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
        animator.enabled = false;
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;

        if (deadSprite != null ) {
            spriteRenderer.sprite = deadSprite;
        }
    }


    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders) {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().isKinematic = true;

        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EnemyMovement enemyMovement = GetComponent<EnemyMovement>();

        if (playerMovement != null) {
            playerMovement.enabled = false;
        }

        if (enemyMovement != null) {
            enemyMovement.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;
        Vector3 bottomEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        
    }

}
