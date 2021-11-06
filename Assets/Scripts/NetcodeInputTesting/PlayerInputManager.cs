using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NetcodeInputTesting
{
    [DisallowMultipleComponent]
    public class PlayerInputManager : MonoBehaviour, Controls.IGameActions
    {
        private Controls _controls;
        
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Game.SetCallbacks(this);
            }
            
            _controls.Game.Enable();
        }

        private void OnDisable() => _controls?.Game.Disable();

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost) return;
            
            NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            PlayerController playerController = playerObject.GetComponent<PlayerController>();
            playerController.OnMove(callbackContext);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
        }
    }
}