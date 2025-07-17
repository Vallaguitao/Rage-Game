using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CoinStatus
{
    Real,
    Fake
}

public class Coins : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;
    public AudioClip getSound;

    public CoinStatus coinStatus;

    // Start is called before the first frame update
    void Start()
    {
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
            gameManager.UpdateScore(150); //not 1000 anymore
            audioManager.PlaySFX(getSound);
            Destroy(gameObject);
        }
    }
}
