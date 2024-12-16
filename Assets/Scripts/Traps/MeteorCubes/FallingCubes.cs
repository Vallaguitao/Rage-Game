using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCubes : MonoBehaviour
{
    [Header("Cube Details")]
    [SerializeField] private float pushForce;
    [SerializeField] private GameObject cube;
    [SerializeField] private Rigidbody2D cubeRb;

    [Header("Push Up Details")]
    [SerializeField] private float cubeThrowHeight = 7f;
    [SerializeField] private float secondsWaitBeforePush = 1.5f;

    [Header("Cube Destruction")]
    [SerializeField] private float destroyOnSeconds = 3f;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManagerScript;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip hitSound; //temporary

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();

        cubeRb = GetComponent<Rigidbody2D>();

        GoUp();

        StartCoroutine(UpApex());
        
        //StartCoroutine(LatePush());
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyOnSeconds);
    }
    void StartPush()
    {
        float cubeThrowXDirection = player.transform.position.x + Random.Range(-1.5f, 1.5f);
        cubeRb.AddForce(new Vector2(cubeThrowXDirection, -10) * pushForce, ForceMode2D.Impulse);
    }

    void GoUp()
    {
        cubeRb.AddForce(new Vector2(-0, cubeThrowHeight) * pushForce, ForceMode2D.Impulse);
    }

    /*
    IEnumerator LatePush()
    {
        yield return new WaitForSeconds(2f);
        pushForce = 5f;
        cubeRb.AddForce(new Vector2(8, -1) * pushForce, ForceMode2D.Impulse);
    }
    */

    IEnumerator UpApex()
    {
        yield return new WaitForSeconds(secondsWaitBeforePush);
        StartPush();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManagerScript.LoseALife();
            audioManager.PlaySFX(hitSound);
            Destroy(gameObject);
        }
    }
}
