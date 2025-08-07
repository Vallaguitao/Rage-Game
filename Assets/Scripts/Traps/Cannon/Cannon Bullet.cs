using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cannonRenderer;
    [SerializeField] private SpriteRenderer bulletRenderer;
    [SerializeField] private Rigidbody2D bulletRigidbody;
    [SerializeField] private int speed = 5;
    bool test;

    [SerializeField] private GameObject fireBulletObject;
    [SerializeField] private SpriteRenderer fireBulletRenderer;
    [SerializeField] private float fireOffset = -1f;

    void Start()
    {
        //cannonRenderer = GameObject.Find("Cannon").GetComponent<SpriteRenderer>();
        cannonRenderer = transform.parent.GetComponent<SpriteRenderer>();
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRenderer = GetComponent<SpriteRenderer>();

        if(cannonRenderer.flipX)
        {
            fireBulletRenderer.flipX = true;
            fireBulletObject.transform.position = new Vector2(fireBulletObject.transform.position.x - fireOffset, fireBulletObject.transform.position.y);
        }
        else
        {
            fireBulletRenderer.flipX = false;
        }
        //print("Cannon render" + cannonRenderer.flipX);
    }

    void Update()
    {
        if (cannonRenderer.flipX)
        {
            bulletRigidbody.velocity = new Vector2(speed, bulletRigidbody.velocity.y);
        }
        else
        {
            bulletRigidbody.velocity = new Vector2(-speed, bulletRigidbody.velocity.y);
        }

        /*
        if (transform.position.x < -13)
        {
            gameObject.SetActive(false);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            gameObject.SetActive(false);
        }
    }
}
