using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouvement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private bool moveWithJoystick = false;

    [SerializeField]
    private float mouseSensitivity = 100f;
    //[SerializeField]
    //private Transform cameraTransform;
    private float xRotation = 0f;


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        InputManager.Instance.RegisterOnJumpInput(jump);
    }

    void OnDestroy()
    {
        InputManager.Instance.UnregisterOnJumpInput(jump);
    }

    void Update()
    {
        if (transform.position.y < 0f) {
            transform.position = new Vector3(0, 0.1f, 0);
            rb.velocity = Vector3.zero;
        }
        Vector3 newVelocity;
        if (moveWithJoystick) {
            Vector3 joystickDirection = new Vector3(UIManager.Instance.Joystick.Direction.x, 0, UIManager.Instance.Joystick.Direction.y);
            newVelocity = joystickDirection * speed;
        } else {
            newVelocity = InputManager.Instance.movementInput * speed;
        }
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;

        RotatePlayer(newVelocity);
    }

    private void jump(InputAction.CallbackContext callbackContext) {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void RotatePlayer(Vector3 movementVelocity) {
        Vector3 horizontalVelocity = new Vector3(movementVelocity.x, 0f, movementVelocity.z);
        if (horizontalVelocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

}
