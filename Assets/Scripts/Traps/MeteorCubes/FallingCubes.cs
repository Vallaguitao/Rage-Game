using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCubes : MonoBehaviour
{

    //transform.InverseTransformPoint(player.transform.position = to get if player is left or right of the sprite

    [Header("Cube Details")]
    [SerializeField] private float pushForce;
    [SerializeField] private float downwardPushForce = -10f;
    [SerializeField] private float rotationValue;
    [SerializeField] private GameObject cube;
    [SerializeField] private Rigidbody2D cubeRb;

    [Header("Push Up Details")]
    [SerializeField] private float cubeThrowHeight = 7f;
    [SerializeField] private float secondsWaitBeforePush = 1.5f;

    [Header("Cube Destruction")]
    [SerializeField] private float destroyOnSeconds = 3f;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform flowerParent;
    [SerializeField] private GameManager gameManagerScript;
    

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip hitSound; //temporary

    [SerializeField] Vector3 startingPosition;

    //Workaround for when paused
    [SerializeField] Vector3 velocityStorage;
    [SerializeField] Vector3 positionStorage;
    [SerializeField] float timeBeforePush = 0;
    [SerializeField] bool isStored;
    void Start()
    {

        //gameObject.transform.position = transform.TransformPoint(startingPosition);
        isStored = false;
        player = GameObject.FindGameObjectWithTag("Player");
        flowerParent = gameObject.transform.parent.gameObject.transform.parent.GetComponent<Transform>();

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();

        cubeRb = GetComponent<Rigidbody2D>();

        GoUp();
        
        //StartCoroutine(UpApex());
        
        //StartCoroutine(LatePush());
    }

    // Update is called once per frame
    void Update()
    {

        UpApex();

        // [Pause] not working properly as of now, ITS NOW WORKING
        if (GameManager.gameManagerScript.isPaused)
        {
            if(!isStored)
            {
                velocityStorage = cubeRb.velocity;
                positionStorage = cubeRb.position;
                isStored = true;
            }
            
            cubeRb.velocity = Vector3.zero;
            //cubeRb.transform.position = positionStorage;
            cubeRb.Sleep();
        }
        else
        {
            if(velocityStorage != Vector3.zero)
            {
                cubeRb.velocity = velocityStorage;
                //cubeRb.position = positionStorage;
                velocityStorage = Vector3.zero;
                positionStorage = Vector3.zero;
                isStored = false;
            }
            cubeRb.WakeUp();
        }
    }

    //push the meteor to the position of the player with -2.5 - 2.5 (formerly -5 - 5) margin of error
    //mistake for the second plant, meteor does not go to the left side
    void StartPush(float playerDistance)
    {
        if (!GameManager.gameManagerScript.isPaused)
        {
            float randomNumber = Random.Range(-2.5f, 2.5f);
            float cubeThrowXDirection = playerDistance + randomNumber;

            cubeRb.AddForce(new Vector2(cubeThrowXDirection, downwardPushForce) * pushForce, ForceMode2D.Impulse);
        }
        
    }

    //tossing the meteor in air
    void GoUp()
    {
        if(!GameManager.gameManagerScript.isPaused)
        {
            cubeRb.AddForce(new Vector2(-0, cubeThrowHeight) * pushForce, ForceMode2D.Impulse);
            transform.Rotate(Vector3.forward * 180);
        }
        
    }

    //When meteor reach the apex at the top
    //IEnumerator UpApex()
    private void UpApex()
    {
        if (!GameManager.gameManagerScript.isPaused)
        {
            //yield return new WaitForSeconds(secondsWaitBeforePush);

            //working around
            timeBeforePush += Time.deltaTime;

            if(timeBeforePush >= secondsWaitBeforePush)
            {
                float playerDistanceFromTheFlower = flowerParent.transform.InverseTransformPoint(player.transform.position).x;

                //sprite rotation if left or right
                if (playerDistanceFromTheFlower < 0)
                {
                    transform.Rotate(Vector3.forward * rotationValue); //delta.deltatime removed
                }
                else if (playerDistanceFromTheFlower > 0)
                {
                    transform.Rotate(Vector3.forward * -rotationValue);
                }
                else
                {
                    transform.Rotate(Vector3.forward * 0);
                }

                StartPush(playerDistanceFromTheFlower);
                timeBeforePush = 0;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameManagerScript.LoseALife();
            audioManager.PlaySFX(hitSound);
            Destroy(gameObject);

            StartCoroutine("PlayerRespawn");
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator PlayerRespawn()
    {
        player.transform.position = startingPosition;
        yield return new WaitForSeconds(1f);
        player.SetActive(true);
    }
}
