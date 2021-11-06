using MultiplayerSetup.Input.InputSystem;
using Unity.Netcode;
using UnityEngine;

namespace MultiplayerSetup.Controllers
{
    [DisallowMultipleComponent]
    public class PlayerController : NetworkBehaviour, IPlayerInputReceiver
    {
        [SerializeField] private MovementController movementController;
        
        public void Move(Vector2 vector2) => movementController.Move(vector2);

        public void Look(Vector2 vector2)
        {
            
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