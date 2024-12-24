using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cannonRenderer;
    [SerializeField] private SpriteRenderer bulletRenderer;
    [SerializeField] private Rigidbody2D bulletRigidbody;
    [SerializeField] private int direction;

    bool test;

    void Start()
    {
        cannonRenderer = GameObject.Find("Cannon").GetComponent<SpriteRenderer>();
        bulletRigidbody = GetComponent<Rigidbody2D>();
        bulletRenderer = GetComponent<SpriteRenderer>();

        direction = (cannonRenderer.flipX) ? 5 : -5;

        print("Cannon render" + cannonRenderer.flipX);
    }

    void Update()
    {

        bulletRigidbody.velocity = new Vector2(direction, bulletRigidbody.velocity.y);

        if (transform.position.x < -13)
        {
            gameObject.SetActive(false);
        }
    }
}
