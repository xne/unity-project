using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private CinemachineInputAxisController inputAxisController;

    private PlayerInput playerInput;

    private OutputChannels cinemachineChannel;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        inputAxisController.PlayerIndex = playerInput.playerIndex;

        var firstChannel = (int)OutputChannels.Channel01;
        cinemachineChannel = (OutputChannels)(firstChannel << playerInput.playerIndex);

        cinemachineBrain.ChannelMask = cinemachineChannel;
        cinemachineCamera.OutputChannel = cinemachineChannel;
    }
}
