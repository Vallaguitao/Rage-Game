using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Cannon cannon;
    [SerializeField] private Rigidbody bulletRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        cannon = GameObject.Find("Cannon").GetComponent<Cannon>();
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector2.left * speed * Time.deltaTime); //will move right when canon is flipped
        

        if (transform.position.x < -13)
        {
            Destroy(gameObject);
        }
    }
}
