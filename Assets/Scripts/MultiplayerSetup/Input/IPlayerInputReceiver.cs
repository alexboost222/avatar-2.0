using UnityEngine;

namespace MultiplayerSetup.Input.InputSystem
{
    public interface IPlayerInputReceiver
    {
        void Move(Vector2 vector2);
        void Look(Vector2 vector2);
        void Jump();
        void PrimaryAction();
        void SecondaryAction();
    }
}