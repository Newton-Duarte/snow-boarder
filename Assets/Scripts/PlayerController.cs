using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 3f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 25f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] AudioClip jumpClip;

    bool canMove = true;
    bool isGrounded = false;

    Rigidbody2D rb;
    AudioSource audioSource;
    SurfaceEffector2D surfaceEffector;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        surfaceEffector = FindAnyObjectByType<SurfaceEffector2D>();
        gameController = FindAnyObjectByType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && gameController.gameStatus == GameStatus.Play)
        {
            RotatePlayer();
            RespondToBoost();
            RespondToJump();
        }
    }

    void RespondToJump()
    {
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            audioSource.PlayOneShot(jumpClip);
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }

    void RespondToBoost()
    {
        float y = Input.GetAxisRaw("Vertical");

        if (y < 0 && surfaceEffector.speed != baseSpeed)
        {
            surfaceEffector.speed = baseSpeed;
        }
        else if (y > 0 && surfaceEffector.speed != boostSpeed)
        {
            surfaceEffector.speed = boostSpeed;
        }
        else if (y == 0)
        {
            surfaceEffector.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.AddTorque(torqueAmount * -x);
    }

    public void DisableControls()
    {
        canMove = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !isGrounded)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && isGrounded)
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            gameController.GetCoin();
            Destroy(collision.gameObject);
        }
    }
}
