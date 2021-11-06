using MultiplayerSetup.Controllers;
using MultiplayerSetup.Input.InputSystem;
using Unity.Netcode;
using UnityEngine;

namespace MultiplayerSetup.Helpers.InputSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerController))]
    public class InputSystemPlayerInputActivator : NetworkBehaviour
    {
        private PlayerController _playerController;

        private void Awake() => _playerController = GetComponent<PlayerController>();

        private void Start()
        {
            if (!IsLocalPlayer) return;

            InputSystemPlayerInputManager inputManager = gameObject.AddComponent<InputSystemPlayerInputManager>();
            inputManager.Receiver = _playerController;
        }
    }
}