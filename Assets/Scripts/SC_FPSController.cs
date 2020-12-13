using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    
    private Vector3 initialPlayerPos;
    private Quaternion initialPlayerRotation;

    [HideInInspector]
    public bool canMove = true;

    private bool pause = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        initialPlayerPos = transform.position;
        initialPlayerRotation = transform.rotation;
    }

    private void Update()
    {
        if (pause) return;
        
        // We are grounded, so recalculate move direction based on axes
        var forward = transform.TransformDirection(Vector3.forward);
        var right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        var isRunning = Input.GetKey(KeyCode.LeftShift);
        var curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxisRaw("Vertical") : 0;
        var curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxisRaw("Horizontal") : 0;
        var movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxisRaw("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxisRaw("Mouse X") * lookSpeed, 0);
        }

        lookSpeed += Input.mouseScrollDelta.y * 0.1f;
        if (lookSpeed < 0.1f)
        {
            lookSpeed = 0.1f;
        }
    }

    public void ResetPosition()
    {
        pause = true;
        transform.position = initialPlayerPos;
        transform.rotation = initialPlayerRotation;
        StartCoroutine(StopPauseAfterAWhile());
    }

    private IEnumerator StopPauseAfterAWhile()
    {
        yield return new WaitForSeconds(0.1f);
        pause = false;
    }
}