using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClouds : MonoBehaviour
{

    [SerializeField] private Transform pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed = 5f;
    private bool moveIt;

    // Start is called before the first frame update
    void Start()
    {
        pointA = transform;
        moveIt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveIt)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            if (transform.position.y >= pointB.transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, pointB.transform.position.y);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            moveIt = true;
        }
        
    }
}
