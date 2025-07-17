using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    //Movement
    [SerializeField] private float horizontalInput; // input
    [SerializeField] private float speed; //base speed
    [SerializeField] private float xRange; // character border limit
    [SerializeField] private float jumpForce; // jump force
    [SerializeField] private bool isOnGround; // flag to check if player is on ground
    [SerializeField] public float gravityModifier; // Modifier to control gravity strength


    [SerializeField] private Rigidbody2D playerRb;

    //camera control
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float cameraNear = 4;
    [SerializeField] private float cameraFar = 11.63f;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip jumpSound; //temporary

    //event managers

    //jump
    [SerializeField] private UnityEvent onJump;

    public float HorizontalInput { get { return horizontalInput; } private set {  } }


    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>(); // get the component
        Physics.gravity *= gravityModifier;

        playerCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        LimitMovement();
        Jump();

        ChangeCameraDistance();
    }

    void LimitMovement()
    {
        /*
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y);
        }
        */
    }

    #region Movement
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");// to get the input to float
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput); // to actually move the character
        playerRb.velocity = new Vector2 (horizontalInput * speed , playerRb.velocity.y); //lemon
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            onJump?.Invoke();

            //OLD CODE
            //playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            //playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y + jumpForce);
            //audioManager.PlaySFX(jumpSound);
            //isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    public void JumpAction()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y + jumpForce);
    }

    public void ChangeOnGround()
    {
        isOnGround = !isOnGround;
    }

    #endregion Movement

    #region CameraControl

    private void ChangeCameraDistance()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(playerCamera.m_Lens.OrthographicSize == cameraNear)
            {
                playerCamera.m_Lens.OrthographicSize = cameraFar;
            }
            else
            {
                playerCamera.m_Lens.OrthographicSize = cameraNear;
            }
            
        }
        
    }


    #endregion
}
