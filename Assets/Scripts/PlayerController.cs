using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalInput; // input
    [SerializeField] private float speed; //base speed
    [SerializeField] private float xRange; // character border limit
    [SerializeField] private float jumpForce; // jump force

    [SerializeField] private bool isOnGround; // flag to check if player is on ground

    [SerializeField] public float gravityModifier; // Modifier to control gravity strength

    [SerializeField] private Rigidbody playerRb;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>(); // get the component
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        LimitMovement();
        Jump();
    }

    void LimitMovement()
    {
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");// to get the input to float
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput); // to actually move the character
        playerRb.velocity = new Vector3 (horizontalInput * speed , playerRb.velocity.y, playerRb.velocity.z); //lemon
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
