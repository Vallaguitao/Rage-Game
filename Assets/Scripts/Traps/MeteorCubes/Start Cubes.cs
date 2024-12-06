using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCubes : MonoBehaviour
{
    [SerializeField] private float pushForce;
    [SerializeField] private GameObject cube;
    [SerializeField] private Rigidbody2D cubeRb;
    // Start is called before the first frame update
    void Start()
    {
        cubeRb = GetComponent<Rigidbody2D>();
        StartPush();
        //StartCoroutine(LatePush());
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,2.5f);
    }
    void StartPush()
    {
        cubeRb.AddForce(new Vector2(-8, -1) * pushForce, ForceMode2D.Impulse);
    }

    /*
    IEnumerator LatePush()
    {
        yield return new WaitForSeconds(2f);
        pushForce = 5f;
        cubeRb.AddForce(new Vector2(8, -1) * pushForce, ForceMode2D.Impulse);
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            print("Game Over");
        }
    }
}
