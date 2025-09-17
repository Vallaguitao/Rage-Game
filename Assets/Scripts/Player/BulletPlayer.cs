using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : Bullets
{
    

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

        }
    }
}
