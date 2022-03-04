using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Scr_Movement_player : MonoBehaviour
{
    float playerHeight = 2f;
    float playerHealth = 100f;
    float playerRegen = 10f;
    float playerMaxHealth = 100f;
    float nextRegen = 0f;
    float regenRate = 2f;
    bool isDead = false;

    public Slider HealthBar;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float sprintSpeed = 10f;
    public float moveSpeed = 4f;
    public float airMultiplier = 1f;
    public float movementMultiplier = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 0f;

    float gravity;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;
    Vector3 jumpMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    public void ReciveDamage(float ammount)
    {
        playerHealth -= ammount;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    } 

    public float GetCurrentHealth()
    {
        return playerHealth;
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 0f;
        playerHealth = playerMaxHealth;
    }


    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        MyInput();
        ControllDrag();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        //gravity
        if (isGrounded)
        {
            gravity = 0f;
        }
        else
        {
            gravity = Time.deltaTime * -9.81f;
        }

        if (Time.time >= nextRegen)
        {
            nextRegen = Time.time + 20f / regenRate;

            if (playerHealth <= playerMaxHealth)
            {
                playerHealth += playerRegen;
            }

            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
        }

        HealthBar.value = playerHealth;

        if (playerHealth <= 0)
        {
            isDead = true;
        }
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        jumpMoveDirection = moveDirection * 0.1f;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ControllDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //sprint speed on flat ground
                rb.AddForce(moveDirection.normalized * sprintSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else
            {
                //walk speed on flat ground
                rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
        }
        else if (isGrounded && OnSlope())
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //sprint speed on slope
                rb.AddForce(slopeMoveDirection.normalized * sprintSpeed * movementMultiplier, ForceMode.Acceleration);
            }
            else
            {
                //walk speed on slope
                rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
            }
        }
        else if (!isGrounded)
        {
            //jumping in mid air force
            rb.AddForce(jumpMoveDirection.normalized * moveSpeed * airMultiplier * 0f, ForceMode.Acceleration);
            rb.AddForce(0, gravity * 17f, 0, ForceMode.Acceleration);
        }
    }
}
