using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 9f;
    [SerializeField] private float accelerationTime = 0.1f;

    [SerializeField] private float jumpHeight = 4f;
    [SerializeField] private float gravity = 90f;
    [SerializeField] private float maxFallSpeed = 24f;

    [SerializeField] private float groundCheckRadius = 0.5f;

    public bool IsGrounded { get; private set; }

    private CharacterController characterController;

    private float acceleration;
    private float jumpSpeed;

    private Vector3 velocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        acceleration = maxSpeed / accelerationTime;
        jumpSpeed = Mathf.Sqrt(2f * jumpHeight * gravity);
    }

    private void FixedUpdate()
    {
        IsGrounded = false;
        var hitColliders = Physics.OverlapSphere(transform.position, groundCheckRadius);
        foreach (var hitCollider in hitColliders)
            if (hitCollider != characterController)
                IsGrounded = true;

        if (!IsGrounded)
            velocity.y = Mathf.MoveTowards(velocity.y, -maxFallSpeed, gravity * Time.deltaTime);

        characterController.Move(transform.TransformDirection(velocity) * Time.deltaTime);
    }

    public void Look(Vector3 value)
    {
        transform.rotation = Quaternion.LookRotation(value);
    }

    public void Move(Vector2 value)
    {
        velocity.x = Mathf.MoveTowards(velocity.x, value.x * maxSpeed, acceleration * Time.deltaTime);
        velocity.z = Mathf.MoveTowards(velocity.z, value.y * maxSpeed, acceleration * Time.deltaTime);
    }

    public void Crouch()
    {
    }

    public void Jump()
    {
        velocity.y = jumpSpeed;
    }

    public void Sprint()
    {
    }
}
