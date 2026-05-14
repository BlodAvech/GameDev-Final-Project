using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerAnimation playerAnimation;
    [SerializeField] private Transform orientation;
    private Vector2 moveInput;
    private float verticalVelocity;

    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Run")]
    [SerializeField] private float runMultiplier = 1.5f;
    [SerializeField] private float airMultiplier = .8f;
    private bool isRunning = false;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 24f;
    [SerializeField] private float fallMultiplier = 3f;
    // private bool canJump = true;
    // [SerializeField] private float jumpReload = .5f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void OnEnable()
    {
        InputManager.Controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        InputManager.Controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        InputManager.Controls.Player.Sprint.performed += ctx => isRunning = true;
        InputManager.Controls.Player.Sprint.canceled += ctx => isRunning = false;

        InputManager.Controls.Player.Jump.performed += ctx => Jump();
    }

    private void Start()
    {
        playerAnimation.MaxSpeed = speed * runMultiplier;
    }

    private void Update()
    {
        Gravity();
        Move();    
    }

    private void Move()
    {
        Vector3 moveDirection = (orientation.forward * moveInput.y + orientation.right * moveInput.x).normalized;
        Vector3 velocity = moveDirection * speed * (isRunning ? runMultiplier : 1f) * (!characterController.isGrounded ? airMultiplier : 1f);
        velocity.y = verticalVelocity;

        characterController.Move(velocity * Time.deltaTime);
        playerAnimation.Speed = new Vector2(velocity.x , velocity.z).magnitude;
    }

    private void Gravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
            return;
        }

        float gravity = Physics.gravity.y;

        if (verticalVelocity < 0f)
            gravity *= fallMultiplier;

        verticalVelocity += gravity * Time.deltaTime;
    }

    private void Jump()
    {
        if(!characterController.isGrounded) return;

        verticalVelocity = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);

        playerAnimation.Jump();
    }
}
