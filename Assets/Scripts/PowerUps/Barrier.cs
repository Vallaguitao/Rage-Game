using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private float throwForce = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            var spawnedBullet = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D bulletRigid = spawnedBullet.GetComponent<Rigidbody2D>();

            Vector2 throwDirection = (GameManager.gameManagerScript.playerControllerScript.PlayerRenderer.flipX == true) 
                ? new Vector2(-1f, 1f) : new Vector2(1f, 1f);

            bulletRigid.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            gameObject.SetActive(false);
            print("Barrier Destroyed");
        }
    }
}
