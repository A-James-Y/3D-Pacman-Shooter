using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public TextMeshProUGUI timerText; // For Timer Display

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 initialPosition;
    private float rotationX = 0;
    private float timer = 0f;
    private bool timerRunning = false;

    public bool canMove = true;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialPosition = transform.position;
    }

    void Update()
    {
        // Timer Logic
        if (timerRunning)
        {
            timer += Time.deltaTime;

        }

        // Start timer on any key press if not already running
        if (Input.anyKey && !timerRunning)
        {
            StartTimer();
        }

        // Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Update the Timer UI
        if (timerText != null)
        {
            timerText.text = "Timer: " + timer.ToString("F2"); // Format as needed
        }
    }

    void StartTimer()
    {
        timerRunning = true;
    }

    void ResetTimer()
    {
        timer = 0f;
        timerRunning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RespawnPlayer();
        }
    }

    private void fall()
    {
        if (playerCamera.transform.position.y == 100f)
        {
            RespawnPlayer();
        }
    }
    void RespawnPlayer()
    {
        characterController.enabled = false;
        transform.position = initialPosition;
        characterController.enabled = true;
        ResetTimer(); // Reset timer on respawn
    }
}
