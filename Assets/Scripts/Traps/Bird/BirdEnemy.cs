using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdEnemy : Bird
{
    //Code can be fixed (Copy paste from other script)
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManagerScript;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip hitSound; 
    [SerializeField] Vector3 startingPosition;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            gameManagerScript.LoseALife();
            audioManager.PlaySFX(hitSound);
            StartCoroutine("PlayerRespawn");
            Destroy(gameObject);

        }
    }

    IEnumerator PlayerRespawn()
    {
        player.transform.position = startingPosition;
        yield return new WaitForSeconds(1f);
        player.SetActive(true);
    }
}
