using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Van : MonoBehaviour
{

    [Header("Van Components")]
    [SerializeField] private Rigidbody2D vanRigid;
    [SerializeField] private SpriteRenderer vanRenderer;

    [Header("Van Details")]
    [SerializeField] private float speed = 20f;

    public float Speed {  get { return speed; } set { speed = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
        vanRenderer = GetComponent<SpriteRenderer>();
        vanRigid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            vanRigid.velocity = new Vector2(speed, vanRigid.velocity.y);
        }

        if(!vanRenderer.isVisible)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.gameManagerScript.PlayerDied();
            gameObject.SetActive(false);
        }
    }
}
