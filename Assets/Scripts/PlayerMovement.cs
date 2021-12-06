using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    protected Joystick leftJoystick, rightJoystick;
    public float StartingSpeed, CurrentSpeed;

    Rigidbody2D rb;
    [SerializeField]
    Shop shop;
    public float dir;
    [SerializeField]
    Animator player, feet;
    bool isWalking;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        shop = FindObjectOfType<Shop>();
        StartingSpeed = shop.DecidePlayerMovementSpeed();
        CurrentSpeed = StartingSpeed;
        leftJoystick = GameObject.FindGameObjectWithTag("Left Stick").GetComponent<Joystick>();
        rightJoystick = GameObject.FindGameObjectWithTag("Right Stick").GetComponent<Joystick>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RotateOnAim();
        RotateOnMovement();
    }

    void FixedUpdate()
    {
        Walk();
    }
    #endregion

    #region Private Methods
    private void RotateOnAim()
    {
        // Some math stuff that I need to learn. Allows right stick to control player rotation
        dir = Mathf.Atan2(rightJoystick.Horizontal, rightJoystick.Vertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, -dir);
    }

    private void RotateOnMovement()
    {
        if (isWalking && rightJoystick.Direction == Vector2.zero)
        {
            dir = Mathf.Atan2(leftJoystick.Horizontal, leftJoystick.Vertical) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -dir);
        }
    }

    private void Walk()
    {
        // Allows left stick to control player movement
        rb.velocity = new Vector2(leftJoystick.Horizontal * CurrentSpeed, leftJoystick.Vertical * CurrentSpeed);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8.41f, 8.41f), Mathf.Clamp(transform.position.y, -4.47f, 4.47f));
        if (rb.velocity != Vector2.zero)
        {
            isWalking = true;
            feet.SetBool("IsMoving", true);
            player.SetBool("IsMoving", true);
        }
        else
        {
            isWalking = false;
            feet.SetBool("IsMoving", false);
            player.SetBool("IsMoving", false);
        }
    }
    #endregion
}
