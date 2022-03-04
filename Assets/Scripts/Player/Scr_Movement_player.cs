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

    //when called it will take the ammount out of the player's health
    public void ReciveDamage(float ammount)
    {
        playerHealth -= ammount;
    }

    //when called it will send a raycast out and return is true if the vector does not return stright up
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

    //when called, it will return the current health on this script
    public float GetCurrentHealth()
    {
        return playerHealth;
    }

    public void Start()
    {
        //rigid body settings are set as well as varibles and the script get the rigidbody
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 0f;
        playerHealth = playerMaxHealth;
    }


    private void Update()
    {
        //is grounded check
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        //runs myinput funtion and controll drag (more over this farther down)
        MyInput();
        ControllDrag();

        //allows the player to jump if they are on the ground and presses the jump key
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        //sets the direction following the slope
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        //gravity for in air
        if (isGrounded)
        {
            gravity = 0f;
        }
        else
        {
            gravity = Time.deltaTime * -9.81f;
        }

        //runs if the delay is over
        if (Time.time >= nextRegen)
        {
            //regenaration delay is set
            nextRegen = Time.time + 20f / regenRate;

            //regenerates the player health if it's lower than the max heath
            if (playerHealth <= playerMaxHealth)
            {
                playerHealth += playerRegen;
            }

            //stops player regeneration and set's it to max heath
            //so it's not over max heath if grater than max health
            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
        }

        //set the health bar to the current health of the player
        HealthBar.value = playerHealth;

        //if the health of the player is lower or equal to 0 then isDead is true
        if (playerHealth <= 0)
        {
            isDead = true;
        }
    }

    //when called it will grab movement input
    void MyInput()
    {
        //grabs key inputs
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        //combines both inputs into one direction
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    //when called then the player will jump in the air
    void Jump()
    {
        jumpMoveDirection = moveDirection * 0.1f;
        //add a jump force to the rigid body component.
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    //when called then drag will be controlled
    void ControllDrag()
    {
        //drag is controlled if in air or ground so the player is not slow falling
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
        //if the player is dead then it will not continue
        if (isDead)
        {
            return;
        }

        //calls the Move player funtion
        MovePlayer();
    }

    //when called then the rigid body will get force applyed
    void MovePlayer()
    {
        //if on the ground but not on a slope
        if (isGrounded && !OnSlope())
        {
            //if left shift is held down
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

        //if the player is on the ground and on a slope
        else if (isGrounded && OnSlope())
        {
            //if left shift is held down
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

        //if the player is not on the ground
        else if (!isGrounded)
        {
            //jumping in mid air force with a downwards force
            rb.AddForce(jumpMoveDirection.normalized * moveSpeed * airMultiplier * 0f, ForceMode.Acceleration);
            rb.AddForce(0, gravity * 17f, 0, ForceMode.Acceleration);
        }
    }
}
