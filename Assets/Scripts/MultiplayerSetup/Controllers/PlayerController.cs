using MultiplayerSetup.Input.InputSystem;
using Unity.Netcode;
using UnityEngine;

namespace MultiplayerSetup.Controllers
{
    [DisallowMultipleComponent]
    public class PlayerController : NetworkBehaviour, IPlayerInputReceiver
    {
        [SerializeField] private MovementController movementController;
        [SerializeField] private CameraLookController cameraLookController;
        
        public void Move(Vector2 vector2) => movementController.Move(vector2);

        public void Look(Vector2 vector2)
        {
            movementController.Look(vector2.x);
            cameraLookController.Look(vector2.y);
        }

        public void Jump() => movementController.Jump();

        public void PrimaryAction()
        {
            
        }

        public void SecondaryAction()
        {
            
        }
    }
}