using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCubes : TrapsCommonality
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

    [Header("References")]
    [SerializeField] private Transform flowerParent;
    [SerializeField] private AudioClip hitSound; //temporary


    //Workaround for when paused
    [SerializeField] Vector3 velocityStorage;
    [SerializeField] Vector3 positionStorage;
    [SerializeField] float timeBeforePush = 0;
    [SerializeField] bool isStored;

    protected override void Start()
    {
        base.Start();

        isStored = false;
        isDestructible = true;
        cubeRb = GetComponent<Rigidbody2D>();

        flowerParent = gameObject.transform.parent.gameObject.transform.parent.GetComponent<Transform>();
        
        GoUp();
        
    }

    void Update()
    {

        UpApex();

        if (GameManager.gameManagerScript.isPaused)
        {
            if(!isStored)
            {
                velocityStorage = cubeRb.velocity;
                positionStorage = cubeRb.position;
                isStored = true;
            }
            
            cubeRb.velocity = Vector3.zero;
            cubeRb.Sleep();
        }
        else
        {
            if(velocityStorage != Vector3.zero)
            {
                cubeRb.velocity = velocityStorage;
                velocityStorage = Vector3.zero;
                positionStorage = Vector3.zero;
                isStored = false;
            }
            cubeRb.WakeUp();
        }
    }

    //push the meteor to the position of the player with -2.5 - 2.5 (formerly -5 - 5) margin of error
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

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            base.OnCollisionEnter2D(collision);
            audioManager.PlaySFX(hitSound);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
