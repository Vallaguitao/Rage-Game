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

    //Movement
    [SerializeField] private float horizontalInput; // input
    [SerializeField] private float speed; //base speed
    [SerializeField] private float xRange; // character border limit
    [SerializeField] private float jumpForce; // jump force
    [SerializeField] private bool isOnGround; // flag to check if player is on ground
    [SerializeField] public float gravityModifier; // Modifier to control gravity strength

    public float Speed { get { return speed; } set { speed = value; } }

    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private SpriteRenderer playerRenderer;

    //camera control
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float cameraNear = 4;
    [SerializeField] private float cameraFar = 11.63f;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip jumpSound; //temporary
    [SerializeField] private CanvasGroup pausedMenu;

    //event managers
    [SerializeField] private UnityEvent onJump;
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onDePause;

    [SerializeField] private EventSystem eventSystem1;

    public float HorizontalInput { get { return horizontalInput; } private set {  } }

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>(); // get the component
        Physics.gravity *= gravityModifier;

        playerCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        eventSystem1 = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        playerRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameManagerScript.isPaused)
        {
            Jump();
            ChangeCameraDistance();
        }

        EscapePress();
    }

    private void FixedUpdate()
    {
        if (!GameManager.gameManagerScript.isPaused)
        {
            //update this someday to the new input system
            Movement();

            //When space is used to press [Resume Button], player also jumps
            
        }

        
    }

    #region Movement
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");// to get the input to float
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput); // to actually move the character
        playerRb.velocity = new Vector2 (horizontalInput * speed , playerRb.velocity.y); //lemon
    
        FlipSprite();
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
        //playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    #endregion Movement

    #region CameraControl / Pause / Restart

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

    private void EscapePress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (pausedMenu != null)
            {

                if (!GameManager.gameManagerScript.isPaused)
                {
                    onPause.Invoke();
                }
                else
                {
                    onDePause.Invoke();
                }


            }
        }
    }

    public void Pause()
    {
        GameManager.gameManagerScript.Pause();
        
    }

    public void UnselectButton()
    {

        eventSystem1.SetSelectedGameObject(null);

    }

    public void RestartStage()
    {

        SceneManager.LoadScene(GameManager.gameManagerScript.CurrentStageIndex);

    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit(); // 
        #endif
    }

    #endregion
}
