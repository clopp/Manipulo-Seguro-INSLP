using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class InputManager : Singleton<InputManager> {
    private GenericXRController inputActions;

    [SerializeField] private Vector2 pressedAndReleaedThreshold = new Vector2(0.1f, 0.9f);
    [SerializeField] private InputActionReference jumpActionReference;
    [SerializeField] private float jumpForce = 500.0f;
    /*
        Right Hand Grip Events
    */

    public static UnityEvent OnGripRightPressed = new UnityEvent();
    public static UnityEvent<float> OnGripRightUpdate = new UnityEvent<float>();
    public static UnityEvent onGripRightReleased = new UnityEvent();
    /*
        Right Hand Trigger Events
    */

    public static UnityEvent OnTriggerRightPressed = new UnityEvent();
    public static UnityEvent<float> OnTriggerRightUpdate = new UnityEvent<float>();
    public static UnityEvent onTriggerRightReleased = new UnityEvent();
    /*
        Left Hand Grip Events
    */
    public static UnityEvent OnGripLeftPressed = new UnityEvent();
    public static UnityEvent<float> OnGripLeftUpdate = new UnityEvent<float>();
    public static UnityEvent onGripLeftReleased = new UnityEvent();

    /*
        Left Hand Trigger Events
    */

    public static UnityEvent OnTriggerLeftPressed = new UnityEvent();
    public static UnityEvent<float> OnTriggerLeftUpdate = new UnityEvent<float>();
    public static UnityEvent onTriggerLeftReleased = new UnityEvent();

    private float rightHandGripValue;
    private float leftHandGripValue;
    private float rightHandTriggerValue;
    private float LeftHandTriggerValue;

    private bool rightGripPressed;
    private bool leftGripPressed;
    private bool rightTriggerPressed;
    private bool LeftTriggerPressed;

    private XRRig _xRRig;
    private CapsuleCollider _collider;
    private Rigidbody _body;

    private bool IsGrounded => Physics.Raycast(
    new Vector2(transform.position.x, transform.position.y + 2.0f),
    Vector3.down, 2.0f);

    /*
        Init
    */

    private void Start() {
        _xRRig = GetComponent<XRRig>();
        _collider = GetComponent<CapsuleCollider>();
        _body = GetComponent<Rigidbody>();
        jumpActionReference.action.performed += OnJump;
    }

    private void Update() {
        var center = _xRRig.cameraInRigSpacePos;
        _collider.center = new Vector3(center.x, _collider.center.y, center.z);
        _collider.height = _xRRig.cameraInRigSpaceHeight;
    }

    private void OnJump(InputAction.CallbackContext obj) {
        if (!IsGrounded) return;
        _body.AddForce(Vector3.up * jumpForce);
    }

    private void Awake() {
        inputActions = new GenericXRController();
        inputActions.RightController.Grip.performed += PressGripRight;
        inputActions.RightController.Trigger.performed += PressTriggerRight;
        inputActions.LeftController.Grip.performed += PressGripLeft;
        inputActions.LeftController.Trigger.performed += PressTriggerLeft;
        inputActions.Enable();
    }

    private void OnEnable() {
    }

    private void OnDisable() {
    }

    private void OnDestroy() {
    }

    private void PressGripRight(InputAction.CallbackContext obj) {
        rightHandGripValue = obj.ReadValue<float>();
        if (rightHandGripValue > pressedAndReleaedThreshold.x && rightHandGripValue < pressedAndReleaedThreshold.y)
        {
            OnGripRightUpdate.Invoke(rightHandGripValue);
            //Debug.Log($"Right Grip {rightHandGripValue}");
        }
        if (!rightGripPressed && rightHandGripValue > pressedAndReleaedThreshold.y)
        {
            OnGripRightPressed.Invoke();
            rightGripPressed = true;
            //Debug.Log($"Right Hand Grip Pressed");
        }
        if (rightGripPressed && rightHandGripValue < pressedAndReleaedThreshold.x)
        {
            onGripRightReleased.Invoke();
            rightGripPressed = false;
            //Debug.Log($"Right Hand Grip Released");
        }
    }

    private void PressGripLeft(InputAction.CallbackContext obj) {
        leftHandGripValue = obj.ReadValue<float>();
        if (leftHandGripValue > pressedAndReleaedThreshold.x && leftHandGripValue < pressedAndReleaedThreshold.y)
        {
            OnGripLeftUpdate.Invoke(leftHandGripValue);
            //Debug.Log($"Left Grip {leftHandGripValue}");
        }
        if (!leftGripPressed && leftHandGripValue > pressedAndReleaedThreshold.y)
        {
            OnGripLeftPressed.Invoke();
            leftGripPressed = true;
            //Debug.Log($"left Hand Grip Pressed");
        }
        if (leftGripPressed && leftHandGripValue < pressedAndReleaedThreshold.x)
        {
            onGripLeftReleased.Invoke();
            leftGripPressed = false;
            //Debug.Log($"left Hand Grip Pressed");
        }
    }

    private void PressTriggerRight(InputAction.CallbackContext obj) {
        rightHandTriggerValue = obj.ReadValue<float>();
        if (rightHandTriggerValue > pressedAndReleaedThreshold.x && rightHandTriggerValue < pressedAndReleaedThreshold.y)
        {
            OnTriggerRightUpdate.Invoke(rightHandTriggerValue);
            //Debug.Log($"Right Trigger{rightHandTriggerValue}");
        }
        if (!rightTriggerPressed && rightHandTriggerValue > pressedAndReleaedThreshold.y)
        {
            OnTriggerRightPressed.Invoke();
            rightTriggerPressed = true;
            //Debug.Log($"Right Trigger Pressed");
        }
        if (rightTriggerPressed && rightHandTriggerValue < pressedAndReleaedThreshold.x)
        {
            onTriggerRightReleased.Invoke();
            rightTriggerPressed = false;
            //Debug.Log($"Right Trigger Released");
        }
    }

    private void PressTriggerLeft(InputAction.CallbackContext obj) {
        LeftHandTriggerValue = obj.ReadValue<float>();
        if (LeftHandTriggerValue > pressedAndReleaedThreshold.x && LeftHandTriggerValue < pressedAndReleaedThreshold.y)
        {
            OnTriggerLeftUpdate.Invoke(LeftHandTriggerValue);
            //Debug.Log($"left Trigger{LeftHandTriggerValue}");
        }
        if (!LeftTriggerPressed && LeftHandTriggerValue > pressedAndReleaedThreshold.y)
        {
            OnTriggerLeftPressed.Invoke();
            LeftTriggerPressed = true;
            //Debug.Log($"left Trigger Pressed");
        }
        if (LeftTriggerPressed && LeftHandTriggerValue < pressedAndReleaedThreshold.x)
        {
            onTriggerLeftReleased.Invoke();
            LeftTriggerPressed = false;
            //Debug.Log($"left Trigger Released");
        }
    }
}