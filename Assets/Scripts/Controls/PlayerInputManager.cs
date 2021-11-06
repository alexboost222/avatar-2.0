using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    [DisallowMultipleComponent]
    public class PlayerInputManager : MonoBehaviour, Controls.IPlayerActions
    {
        private Controls _controls;
        
        public void OnMove(InputAction.CallbackContext context)
        {
            if (!CanApplyInput(out PlayerController playerController)) return;
            
            playerController.Move(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (!CanApplyInput(out PlayerController playerController)) return;
            
            playerController.Look(context.ReadValue<Vector2>());
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (!CanApplyInput(out PlayerController playerController) || !context.performed) return;
            
            playerController.Fire();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!CanApplyInput(out PlayerController playerController) || !context.performed) return;
            
            playerController.Jump();
        }

        public void OnTakeSource(InputAction.CallbackContext context)
        {
            if (!CanApplyInput(out PlayerController playerController) || !context.performed) return;
            
            playerController.SecondaryAction();
        }

        private static bool CanApplyInput(out PlayerController playerController)
        {
            playerController = null;
            
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost) return false;
            
            NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            
            return !playerObject.TryGetComponent(out playerController) ? false : playerController;
        }

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            
            _controls.Player.Enable();
        }

        private void OnDisable() => _controls?.Disable();
    }
}