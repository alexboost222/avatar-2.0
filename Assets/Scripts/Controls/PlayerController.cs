using System;
using UnityEngine;

namespace Controls
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody Rb => rb;
        public StatsController Stats => stats;
        
        public event Action PrimaryAction;
        public event Action TakeSource;

        public bool IsOnGround { get; private set; }
        public LayerMask groundMask;
        public float groundDistance;
        public float groundDampen;
        
        public float moveSpeed;
        public float inAirSpeed;
        public float jumpSpeed;

        public Vector2 lookSpeed;

        [SerializeField]
        private StatsController stats;
        [SerializeField]
        private Transform head;
        [SerializeField]
        private Rigidbody rb;
        
        private Vector2 _moveVector;
        private Vector2 _lookVector;

        private void Update()
        {
            rb.drag = IsOnGround ? groundDampen : 0;
            
            HandleMovement(_moveVector);
            HandleLook(_lookVector);
            CheckIfOnGround();
        }

        private void HandleLook(Vector2 lookVector)
        {
            head.transform.Rotate(Vector3.right, -lookVector.y * lookSpeed.y);
            transform.Rotate(Vector3.up, lookVector.x * lookSpeed.x);
        }

        private void CheckIfOnGround() => IsOnGround = Physics.Raycast(transform.position + Vector3.up * groundDistance,
            Vector3.down, groundDistance * 2, groundMask);

        private void HandleMovement(Vector2 movement)
        {
            float speed = IsOnGround ? moveSpeed : inAirSpeed;
            
            rb.AddRelativeForce(Vector3.forward * (movement.y * Time.deltaTime * speed * 100));
            rb.AddRelativeForce(Vector3.right * (movement.x * Time.deltaTime * speed * 100));
        }

        public void Move(Vector2 vec) => _moveVector = vec;

        public void Look(Vector2 vec) => _lookVector = vec;

        public void Fire() => PrimaryAction?.Invoke();

        public void Jump()
        {
            if (!IsOnGround) return;
            
            rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * jumpSpeed);
        }

        public void SecondaryAction() => TakeSource?.Invoke();
    }
}
