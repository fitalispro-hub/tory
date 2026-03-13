using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector3 minScreenBounds;
    private Vector3 maxScreenBounds;

    // Reference to the generated input actions
    private InputSystem_Actions inputActions;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        this.maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void OnEnable()
    {
        // Create the input actions instance
        inputActions = new InputSystem_Actions();
        
        // Enable the Player action map
        inputActions.Player.Enable();
        
        // Subscribe to the Move action
        inputActions.Player.Move.performed += Move;
        inputActions.Player.Move.canceled += Move;
    }

    void OnDisable()
    {
        // Unsubscribe and disable when object is disabled
        inputActions.Player.Move.performed -= Move;
        inputActions.Player.Move.canceled -= Move;
        inputActions.Player.Disable();
        inputActions.Dispose();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        transform.position = new Vector3(
            Math.Clamp(rb.position.x, minScreenBounds.x, maxScreenBounds.x), 
            Math.Clamp(rb.position.y, minScreenBounds.y, maxScreenBounds.y), 
            0f
        );
    }

    // Callback for movement input
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}