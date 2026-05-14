using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerAnimation))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerAnimation playerAnimation;
    [SerializeField] private Transform orientation;
    [SerializeField] private float speed;
    private Vector2 moveInput;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimation = GetComponent<PlayerAnimation>();

        InputManager.Controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        InputManager.Controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void Start()
    {
        playerAnimation.MaxSpeed = speed;
    }

    private void Update()
    {
        Gravity();
        Move();    
    }

    private void Move()
    {
        Vector3 moveDirection = (orientation.forward * moveInput.y + orientation.right * moveInput.x).normalized;
        Vector3 velocity = moveDirection * speed;
        velocity.y = verticalVelocity;

        characterController.Move(velocity * Time.deltaTime);
        playerAnimation.Speed = new Vector2(velocity.x , velocity.z).magnitude;
    }

    private void Gravity()
    {
        if(characterController.isGrounded && verticalVelocity < 0) verticalVelocity = -2f;
        else verticalVelocity += Physics.gravity.y * Time.deltaTime;
    }


}
