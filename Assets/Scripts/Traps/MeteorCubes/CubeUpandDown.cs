using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeUpandDown : TrapsCommonality
{
    public Rigidbody2D rigidBody;
    protected override void Start()
    {

        base.Start();

        isDestructible = false;
        rigidBody = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        if (GameManager.gameManagerScript.isPaused)
        {
            rigidBody.Sleep();
        }
        else
        {
            rigidBody.WakeUp();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            base.TrapsPlayerDied();
        }
        //else if (collision.gameObject.CompareTag("Ground"))
        else
        {
            Destroy(gameObject);
        }
    }

}
