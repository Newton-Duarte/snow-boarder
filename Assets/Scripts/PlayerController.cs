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
    [SerializeField] AudioClip flipClip;

    float xAxis;
    float yAxis;
    float flipTime;
    float minZAngleToFlip = 300;

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
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");

            RotatePlayer();
            RespondToBoost();
            RespondToJump();
            DetectFlip();
        }
    }

    void DetectFlip()
    {
        if (!isGrounded)
        {
            if (xAxis != 0)
            {
                float zAngle = transform.eulerAngles.z;
                flipTime += Time.deltaTime;
            
                if (flipTime >= 1 && zAngle > minZAngleToFlip)
                {
                    flipTime = 0;
                    Flip();
                }
            }
            else if (flipTime > 0)
            {
                flipTime = 0;
            }
        }
    }

    void Flip()
    {
        audioSource.PlayOneShot(flipClip);
        gameController.SetScore(100);
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
        if (yAxis < 0 && surfaceEffector.speed != baseSpeed)
        {
            surfaceEffector.speed = baseSpeed;
        }
        else if (yAxis > 0 && surfaceEffector.speed != boostSpeed)
        {
            surfaceEffector.speed = boostSpeed;
        }
        else if (yAxis == 0)
        {
            surfaceEffector.speed = baseSpeed;
        }
    }

    void RotatePlayer()
    {
        rb.AddTorque(torqueAmount * -xAxis);
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
            gameController.SetScore();
            Destroy(collision.gameObject);
        }
    }
}
