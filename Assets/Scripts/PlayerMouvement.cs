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

    void Start()
    {
        InputManager.Instance.RegisterOnJumpInput(jump);
    }

    void OnDestroy()
    {
        InputManager.Instance.UnregisterOnJumpInput(jump);
    }

    void Update()
    {
        if (transform.position.y < -10.0f) {
            transform.position = new Vector3(0, 0.5f, 0);
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
    }

    private void jump(InputAction.CallbackContext callbackContext) {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
