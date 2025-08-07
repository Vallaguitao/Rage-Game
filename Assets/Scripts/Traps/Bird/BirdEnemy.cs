using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdEnemy : Bird
{
    //Code can be fixed (Copy paste from other script)
    [SerializeField] private GameManager gameManagerScript;
    [SerializeField] private AudioClip hitSound; 

    protected override void Start()
    {
        base.Start();
        gameManagerScript = GameManager.gameManagerScript;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            gameManagerScript.PlayerDied();
            //audioManager.PlaySFX(hitSound);
            Destroy(gameObject);

        }
    }

}
