using System.Collections;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform connection;
    public GameObject black;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteRendererbig;
    public SpriteRenderer spriteRendererfire;
    public KeyCode enterKeyCode = KeyCode.S;
    public KeyCode enterKeyCode2;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    public AudioSource pipe;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (Input.GetKey(enterKeyCode) || Input.GetKey(enterKeyCode2)) {
                pipe.Play();
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return MoveToPosition(player, enteredPosition, enteredScale);
        black.SetActive(true);
        spriteRenderer.sortingOrder = 0;
        spriteRendererbig.sortingOrder = 0;
        spriteRendererfire.sortingOrder = 0;
        yield return new WaitForSeconds(1f);
        spriteRenderer.sortingOrder = 4;
        spriteRendererbig.sortingOrder = 4;
        spriteRendererfire.sortingOrder = 4;
        black.SetActive(false);

        Camera.main.GetComponent<SideScrolling>().SetUnderground();

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return MoveToPosition(player, connection.position + exitDirection, Vector3.one);
        }

        player.position = connection.position;
        player.localScale = Vector3.one;
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator MoveToPosition(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;
    }

}
