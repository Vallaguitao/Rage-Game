using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallChecker : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] Vector3 startingPosition;
    [SerializeField] GameManager gameManager;

    private AudioManager audioManager;
    [SerializeField] AudioClip fallSound;

    [SerializeField] protected Vector3 boxSize = new Vector3(3f, 3f, 3f);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < transform.position.y)
        {
            PlayerDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        gameManager.LoseALife();
        audioManager.PlaySFX(fallSound);

        StartCoroutine("PlayerRespawn");
    }

    IEnumerator PlayerRespawn()
    {
        player.transform.position = startingPosition;
        yield return new WaitForSeconds(1f);
        player.GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(0, 1, 0, 1f);
        Gizmos.DrawCube(transform.position, boxSize);
    }

}
