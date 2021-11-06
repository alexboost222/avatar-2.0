using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controls
{
    public class PlayerController : MonoBehaviour, Controls.IPlayerActions
    {
        public Rigidbody Rb => rb;
        public StatsController Stats => stats;
        
        public event Action Fire;
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
        
        private Controls _controls;
        
        private Vector2 _moveVector;
        private Vector2 _lookVector;
        
        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            
            _controls.Player.Enable();
        }

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

        private void CheckIfOnGround()
        {
            IsOnGround = Physics.Raycast(transform.position + Vector3.up * groundDistance, Vector3.down, groundDistance * 2, groundMask);
        }

        private void HandleMovement(Vector2 movement)
        {
            float speed = IsOnGround ? moveSpeed : inAirSpeed;
            
            rb.AddRelativeForce(Vector3.forward * (movement.y * Time.deltaTime * speed * 100));
            rb.AddRelativeForce(Vector3.right * (movement.x * Time.deltaTime * speed * 100));
        }

        public void OnMove(InputAction.CallbackContext context)
            => _moveVector = context.ReadValue<Vector2>();

        public void OnLook(InputAction.CallbackContext context)
            => _lookVector = context.ReadValue<Vector2>();

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
                Fire?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!IsOnGround)
                return;
            
            rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * jumpSpeed);
        }

        public void OnTakeSource(InputAction.CallbackContext context)
        {
            if (context.performed)
                TakeSource?.Invoke();
        }
    }
}
