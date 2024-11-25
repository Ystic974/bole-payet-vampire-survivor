using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private InputActionReference MovementAction = null;
    [SerializeField]
    private InputActionReference JumpAction = null;
    [SerializeField]
    private InputActionReference MouseAction = null;


    private static InputManager _instance = null;
    public static InputManager Instance { get { return _instance; } }

    public Vector3 movementInput { get; private set; }

    public Action<Vector2> FingerDownAction = null;

    private void Awake() {
        if(_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        Vector2 moveInput = MovementAction.action.ReadValue<Vector2>();
        movementInput = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void OnFingerDown(Finger finger) {
        Vector2 screenPosition = finger.currentTouch.screenPosition;
        RectTransform joystickRectTransform = UIManager.Instance.Joystick.transform as RectTransform;

        bool isInX = joystickRectTransform.offsetMin.x <= screenPosition.x && screenPosition.x <= joystickRectTransform.offsetMax.x;
        bool isInY = joystickRectTransform.offsetMin.y <= screenPosition.y && screenPosition.y <= joystickRectTransform.offsetMax.y;

        if (!isInX || !isInY) {
            FingerDownAction.Invoke(screenPosition);
        }
    }

    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();

        Touch.onFingerDown += OnFingerDown;
    }

    private void OnDisable()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Enable();

        Touch.onFingerDown -= OnFingerDown;
    }

    public void RegisterOnJumpInput(Action<InputAction.CallbackContext> onJumpAction) {
        JumpAction.action.performed += onJumpAction;
    }

    public void UnregisterOnJumpInput(Action<InputAction.CallbackContext> onJumpAction) {
        JumpAction.action.performed -= onJumpAction;
    }

    public void RegisterOnClick(Action<InputAction.CallbackContext> onClickAction) {
        MouseAction.action.performed += onClickAction;
    }

    public void UnregisterOnClick(Action<InputAction.CallbackContext> onClickAction) {
        MouseAction.action.performed -= onClickAction;
    }
}
