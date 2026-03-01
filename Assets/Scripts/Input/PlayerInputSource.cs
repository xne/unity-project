using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputSource : MonoBehaviour, IInputSource
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineInputAxisController inputAxisController;

    private PlayerInput playerInput;

    private InputData inputData;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        var playerIndex = playerInput.playerIndex;
        inputAxisController.PlayerIndex = playerIndex;

        var firstChannel = (int)OutputChannels.Channel01;
        var channel = (OutputChannels)(firstChannel << playerIndex);

        cinemachineBrain.ChannelMask = channel;
        cinemachineCamera.OutputChannel = channel;

        inputData.Look = Vector3.forward;
    }

    public InputData GetInput()
    {
        inputData.Look = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        return inputData;
    }

    private void OnMove(InputValue value) => inputData.Move = value.Get<Vector2>();
    private void OnAttack(InputValue value) => inputData.Attack = value.isPressed;
    private void OnCrouch(InputValue value) => inputData.Crouch = value.isPressed;
    private void OnJump(InputValue value) => inputData.Jump = value.isPressed;
    private void OnSprint(InputValue value) => inputData.Sprint = value.isPressed;
    private void OnZoom(InputValue value) => inputData.Zoom = value.isPressed;
}
