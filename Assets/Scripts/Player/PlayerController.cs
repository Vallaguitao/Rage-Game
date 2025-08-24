using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("Script References")]
    [SerializeField] private PlayerInputHandler playerInputHandlerScript;
    [SerializeField] private PlayerPowerUp playerPowerUpScript;

    //Movement
    [Header("Movement")]
    [SerializeField] private float horizontalInput; // input
    [SerializeField] private float speed; //base speed
    [SerializeField] private float xRange; // character border limit
    [SerializeField] private float jumpForce; // jump force
    [SerializeField] private bool isOnGround; // flag to check if player is on ground
    [SerializeField] public float gravityModifier; // Modifier to control gravity strength

    public float Speed { get { return speed; } set { speed = value; } }

    [Header("Player Components")]
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private SpriteRenderer playerRenderer;

    //camera control
    [Header("Camera Control")]
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float cameraNear = 4;
    [SerializeField] private float cameraFar = 11.63f;

    [Header("Audio")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip jumpSound; //temporary

    //event managers
    [Header("Event Manager")]
    [SerializeField] private UnityEvent onJump;

    //[SerializeField] private EventSystem eventSystem1;

    public float HorizontalInput { get { return horizontalInput; } private set {  } }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>(); // get the component
        Physics.gravity *= gravityModifier;

        playerCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        //eventSystem1 = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        playerRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = playerInputHandlerScript.MoveInput.x;

        if (!GameManager.gameManagerScript.isPaused)
        {
            //Jump();
            //ChangeCameraDistance();
            
            playerInputHandlerScript.jumpInput.performed += context => Jump();
            playerInputHandlerScript.CameraChangeInput.performed += context => ChangeCameraDistance(); ;
            playerInputHandlerScript.PowerUpInput.performed += context => playerPowerUpScript.BarrierPowerUp();

        }

        //PauseGame();
        playerInputHandlerScript.PauseInput.performed += context => PauseGame();
    }

    private void FixedUpdate()
    {
        if (!GameManager.gameManagerScript.isPaused)
        {
            Movement();

            //When space is used to press [Resume Button], player also jumps
            
        }

        
    }

    #region Movement
    void Movement()
    {
        playerRb.velocity = new Vector2 (horizontalInput * speed , playerRb.velocity.y); 
    
        FlipSprite();
    }

    void Jump()
    {
        if ((isOnGround) && (!GameManager.gameManagerScript.isPaused)) // i do not know why the 2nd condition is needed if it is checking in update
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

    private void FlipSprite()
    {
        if(horizontalInput < 0)
        {
            playerRenderer.flipX = true;
        }
        else if(horizontalInput > 0)
        {
            playerRenderer.flipX = false;
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

    public void StopMoving()
    {
        playerRb.velocity = new Vector2(0 ,0);
        horizontalInput = 0;
    }

    public void OnPause()
    {
        StopMoving();
        playerRb.Sleep();
    }

    public void OnDePause()
    {
        playerRb.WakeUp();
    }

    #endregion Movement

    #region CameraControl / Pause 

    private void ChangeCameraDistance()
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

    private void PauseGame()
    {
        if (GameManager.gameManagerScript.PausedMenu != null)
        {

            if (!GameManager.gameManagerScript.isPaused)
            {
                GameManager.gameManagerScript.OnPause.Invoke();
            }
            else
            {
                GameManager.gameManagerScript.OnDePause.Invoke();
            }


        }
    }

    #endregion
}
