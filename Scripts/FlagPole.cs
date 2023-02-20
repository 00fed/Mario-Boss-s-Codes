using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public GameObject flag;
    public Transform poleBottom;
    public Transform castle;
    public AudioSource flaga;
    public AudioSource castlea;
    public float speed = 6f;
    public bool anim;
    public bool a;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Player"))

        {   
            flaga.Play();
            StartCoroutine(MoveToPosition(flag.transform, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(other.transform));
            GameManager.Instance.FourHundred();
        }
    }

    private IEnumerator MoveToPosition(Transform transform, Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.125f)
        {
            a = true;
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
            GameManager.Instance.Castle();
            
            yield return null;
        }
        
        transform.position = position;
    }

    public IEnumerator LevelCompleteSequence(Transform player)
    {   
        anim = true;
        player.GetComponent<Player>().enabled = false;
        
        yield return MoveToPosition(player, poleBottom.position);
        yield return MoveToPosition(player, player.position + Vector3.right);
        castlea.Play();
        yield return MoveToPosition(player, player.position + Vector3.right + Vector3.down);
        yield return MoveToPosition(player, castle.position);

        player.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0;
        GameManager.Instance.Pause();
    }

}