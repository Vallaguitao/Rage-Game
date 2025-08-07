using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsCommonality : MonoBehaviour
{

    [Header("References")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected SpriteRenderer playerRenderer;
    [SerializeField] protected GameManager gameManagerScript;

    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected Vector3 startingPosition;

    [SerializeField] protected bool isDestructible;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRenderer = player.GetComponent<SpriteRenderer>();
        gameManagerScript = GameManager.gameManagerScript;
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDied();
        }
        
    }

    protected virtual IEnumerator PlayerRespawn()
    {
        player.transform.position = gameManagerScript.StartingPosition;
        yield return new WaitForSeconds(1f);
        //player.SetActive(true);
        playerRenderer.enabled = true;
    }

    protected virtual void PlayerDied()
    {
        if (isDestructible)
        {
            Destroy(gameObject);
        }

        playerRenderer.enabled = false;
        gameManagerScript.LoseALife();


        StartCoroutine("PlayerRespawn");

        playerRenderer.enabled = true;
        //put an invinsibility period
    }
}
