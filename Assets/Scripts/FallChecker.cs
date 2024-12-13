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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            gameManager.LoseALife();
            audioManager.PlaySFX(fallSound);

            StartCoroutine("PlayerRespawn");
        }
    }

    IEnumerator PlayerRespawn()
    {
        player.transform.position = startingPosition;
        yield return new WaitForSeconds(1f);
        player.SetActive(true);
    }
}
