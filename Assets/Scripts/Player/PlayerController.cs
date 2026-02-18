using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Multiplayer
    public int PlayerIndex { get; private set; } = -1;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineInputAxisController inputAxisController;

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 9f;
    [SerializeField] private float accelerationTime = 0.1f;

    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravity = 90f;
    [SerializeField] private float maxFallSpeed = 24f;

    private float Acceleration => maxSpeed / accelerationTime;
    private float JumpSpeed => Mathf.Sqrt(2f * jumpHeight * gravity);

    // Components
    private PlayerInput playerInput;
    private CharacterController characterController;

    // Camera
    private OutputChannels cinemachineChannel;

    // Movement
    private Vector3 direction;
    private Vector3 velocity;

    private void Awake()
    {
        // Components
        playerInput = GetComponent<PlayerInput>();
        characterController = GetComponent<CharacterController>();

        // Input
        inputAxisController.PlayerIndex = playerInput.playerIndex;

        // Camera
        var firstChannel = (int)OutputChannels.Channel01;
        cinemachineChannel = (OutputChannels)(firstChannel << playerInput.playerIndex);

        cinemachineBrain.ChannelMask = cinemachineChannel;
        cinemachineCamera.OutputChannel = cinemachineChannel;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        if (direction.magnitude > 0f)
            Turn();

        Move(deltaTime);
        Fall(deltaTime);

        var worldVelocity = transform.TransformDirection(velocity);
        characterController.Move(worldVelocity * deltaTime);
    }

    private void Turn()
    {
        var direction = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        var rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void Move(float deltaTime)
    {
        var acceleration = Acceleration * deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, direction.x * maxSpeed, acceleration);
        velocity.z = Mathf.MoveTowards(velocity.z, direction.z * maxSpeed, acceleration);
    }

    private void Fall(float deltaTime)
    {
        velocity.y = Mathf.MoveTowards(velocity.y, -maxFallSpeed, gravity * deltaTime);
    }

    public void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        direction = new Vector3(input.x, 0f, input.y);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
            velocity.y = JumpSpeed;
    }
}
