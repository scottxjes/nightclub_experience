using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed = 5f;
    public float rotSpeed = 1f;
    public float jumpForce = 10f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.2f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private bool isGrounded;
    private Vector3 velocity;

    private bool isDancing = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Dance();
            anim.SetFloat("Walk", 0f);
        }
        else
        {
            isDancing = false;
            anim.SetBool("Dance", isDancing);
            Walk();
        }



    }


    private void Walk()
    {
        // Check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Get input for character movement
        float horizontalInput = 0f;// Input.GetAxis("Horizontal");
        float verticalInput = Mathf.Clamp(Input.GetAxis("Vertical"),0f,1f);

        // Calculate move direction based on camera orientation
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        moveDirection = (camForward * verticalInput + camRight * horizontalInput).normalized;

        moveDirection = Vector3.Lerp(this.transform.forward*verticalInput, moveDirection, rotSpeed * Time.deltaTime);

        /*
        this.transform.Rotate(Vector3.up *horizontalInput*rotSpeed*Time.deltaTime);

        moveDirection = (this.transform.forward * verticalInput).normalized;
        */

        // Rotate the character in the direction of movement
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

        // Apply gravity
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; // A small negative value to help with ground sticking
        }
        velocity.y += gravity * Time.deltaTime;

        // Move the character with improved controls
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Apply the final vertical movement
        characterController.Move(velocity * Time.deltaTime);


        //animation
        anim.SetFloat("Walk", verticalInput);

    }



    private void Dance()
    {
        isDancing = true;
        anim.SetBool("Dance",isDancing);
    }

    public void SetAnimator(Animator anim)
    {
        this.anim = anim;
    }

    public void GO()
    {

    }


}

