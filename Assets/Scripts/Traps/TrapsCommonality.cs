using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsCommonality : MonoBehaviour
{

    [Header("References")]
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameManager gameManagerScript;
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] protected bool isDestructible;

    private void Awake()
    {
        gameManagerScript = GameManager.gameManagerScript;
        player = gameManagerScript.player;
        audioManager = gameManagerScript.audioManager;
    }
    protected virtual void Start()
    {
        
        
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TrapsPlayerDied();
        }
        
    }

    protected virtual void TrapsPlayerDied()
    {
        if (isDestructible)
        {
            Destroy(gameObject);
        }

        gameManagerScript.PlayerDied();
    }
}
