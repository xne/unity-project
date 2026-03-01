using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterAgent : MonoBehaviour
{
    private IInputSource inputSource;
    private CharacterMovement movement;

    private void Awake()
    {
        inputSource = GetComponent<IInputSource>();
        movement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        var input = inputSource.GetInput();

        movement.Look(input.Look);
        movement.Move(input.Move);

        if (input.Crouch)
            movement.Crouch();

        if (input.Jump)
            if (movement.IsGrounded)
                movement.Jump();

        if (input.Sprint)
            movement.Sprint();
    }
}
