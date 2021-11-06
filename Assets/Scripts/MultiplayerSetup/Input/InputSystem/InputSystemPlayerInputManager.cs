using UnityEngine;
using UnityEngine.InputSystem;

namespace MultiplayerSetup.Input.InputSystem
{
    [DisallowMultipleComponent]
    public class InputSystemPlayerInputManager : MonoBehaviour, Controls.IMultiplierSetupActions
    {
        private Controls _controls;
        
        public IPlayerInputReceiver Receiver { get; set; }
        
        public void OnSecondaryAction(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            Receiver?.SecondaryAction();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            Receiver?.Jump();
        }

        public void OnPrimaryAction(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            Receiver?.PrimaryAction();
        }

        public void OnLook(InputAction.CallbackContext context) => Receiver?.Look(context.ReadValue<Vector2>());

        public void OnMove(InputAction.CallbackContext context) => Receiver?.Move(context.ReadValue<Vector2>());

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.MultiplierSetup.SetCallbacks(this);
            }
            
            _controls.MultiplierSetup.Enable();
        }

        private void OnDisable() => _controls?.Disable();
    }
}