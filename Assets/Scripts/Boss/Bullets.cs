using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    [SerializeField] private float speed;

    // Start is called before the first frame update
    protected virtual void  Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void  Update()
    {
        if(!GameManager.gameManagerScript.isPaused)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.gameManagerScript.PlayerDied();
            Destroy(gameObject);
            
        }
    }
}
