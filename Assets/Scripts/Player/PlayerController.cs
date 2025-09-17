using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    #region variables

    [Header("Script References")]
    [SerializeField] private PlayerInputHandler playerInputHandlerScript;
    [SerializeField] private PlayerPowerUp playerPowerUpScript;

    public PlayerInputHandler PlayerInputHandlerScript { get { return PlayerInputHandlerScript; } set { PlayerInputHandlerScript = value; } }
    public InputAction CancelActionController { get { return playerInputHandlerScript.cancelInput ; } set { playerInputHandlerScript.cancelInput = value; } }
    public InputAction InteractActionController { get { return playerInputHandlerScript.InteractInput; } set { playerInputHandlerScript.InteractInput = value; } }

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

    public SpriteRenderer PlayerRenderer { get {  return playerRenderer; } private set { } }

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

    [Header("Bullet Properties")]
    [SerializeField] private int ammo;
    [SerializeField] private GameObject ammoObject;
    [SerializeField] private TextMeshProUGUI ammoCountText;

    public float HorizontalInput { get { return horizontalInput; } private set {  } }

    #endregion

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>(); 
        Physics.gravity *= gravityModifier;

        playerCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        //eventSystem1 = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        playerRenderer = GetComponent<SpriteRenderer>();

        playerInputHandlerScript.jumpInput.performed += Jump;
        playerInputHandlerScript.CameraChangeInput.performed += ChangeCameraDistance;
        playerInputHandlerScript.PowerUpInput.performed += playerPowerUpScript.BarrierPowerUp;
        playerInputHandlerScript.shootInput.performed += Shoot;

        //the code is a mess
        playerInputHandlerScript.PauseInput.performed += PauseGame;

        string currentSceneCheck = SceneManager.GetActiveScene().name;

        if (currentSceneCheck.Equals("Main Menu") || currentSceneCheck.Equals("Start Menu"))
        {
            playerInputHandlerScript.PauseInput.performed -= PauseGame;
        }

        ammo = 0;
}

    void Update()
    {

        horizontalInput = playerInputHandlerScript.MoveInput.x;

        if (!GameManager.gameManagerScript.isPaused)
        {
            
        }

    }

    private void FixedUpdate()
    {
        if (!GameManager.gameManagerScript.isPaused)
        {

            Movement();
            
        }

        
    }

    #region Movement
    void Movement()
    {
        playerRb.velocity = new Vector2 (horizontalInput * speed , playerRb.velocity.y); 
    
        FlipSprite();
    }

    void Jump(InputAction.CallbackContext context)
    {
        if ((isOnGround) && (!GameManager.gameManagerScript.isPaused)) // i do not know why the 2nd condition is needed if it is checking in update
        {
            onJump?.Invoke();
        }
    }

    public void JumpAction()
    {
        print("Jump");
        playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y + jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if(collision.gameObject.CompareTag("BulletShell"))
        {
            ammo += 1;
            Destroy(collision.gameObject);

            if(ammoCountText != null)
            {
                ammoCountText.SetText($"X{ammo}");
            }
            else
            {
                print("No Ammo Text");
            }
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

    

    public void ChangeOnGround()
    {
        isOnGround = !isOnGround;
    }

    public void StopMoving()
    {
        playerRb.velocity = new Vector2(0 ,0);
        horizontalInput = 0;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        if(ammo > 0)
        {

            float offset = (playerRenderer.flipX == true) ? -1 : 1;

            Vector2 positionOffset = new Vector2(transform.position.x + offset, transform.position.y);

            var spawnedBullet = Instantiate(ammoObject, positionOffset, ammoObject.transform.rotation);

            if (offset > 0)
            {
                spawnedBullet.transform.Rotate(0, 0, 180f);
            }

            ammo--;
            
            if(ammoCountText != null)
            {
                ammoCountText.SetText($"X{ammo}");
            }
            else
            {
                print("No Ammo Text");
            }
        }
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

    private void ChangeCameraDistance(InputAction.CallbackContext context)
    {
        print("Camera");
        if (playerCamera.m_Lens.OrthographicSize == cameraNear)
        {
            playerCamera.m_Lens.OrthographicSize = cameraFar;
        }
        else
        {
            playerCamera.m_Lens.OrthographicSize = cameraNear;
        }
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        print("Paused");
        if (GameManager.gameManagerScript.PausedMenu != null)
        {

            if (!GameManager.gameManagerScript.isPaused)
            {
                GameManager.gameManagerScript.OnPause?.Invoke();
            }
            else
            {
                GameManager.gameManagerScript.OnDePause?.Invoke();
            }

        }
        else
        {
            print("Paused No PausedMenu");
        }
    }

    #endregion

    public void CancelledControl()
    {
        playerInputHandlerScript.jumpInput.performed -= Jump;
        playerInputHandlerScript.CameraChangeInput.performed -= ChangeCameraDistance;
        playerInputHandlerScript.PowerUpInput.performed -= playerPowerUpScript.BarrierPowerUp;
        playerInputHandlerScript.shootInput.performed -= Shoot;

        playerInputHandlerScript.PauseInput.performed -=  PauseGame;

    }

    //I know there is a better way but I don't know
    public void TimelinePlayingDisablePause()
    {
        playerInputHandlerScript.PauseInput.performed -= PauseGame;

    }

    public void TimelineDonePlayingEnablePause()
    {
        playerInputHandlerScript.PauseInput.performed += PauseGame;

    }
}
