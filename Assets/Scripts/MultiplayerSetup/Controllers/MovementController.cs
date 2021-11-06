using Unity.Netcode;
using UnityEngine;

namespace MultiplayerSetup.Controllers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : NetworkBehaviour
    {
        [Header("Ground check")]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float groundDistance;

        [Header("Movement params")]
        [SerializeField] private float groundSpeed;
        [SerializeField] private float airSpeed;
        [SerializeField] private float jumpSpeed;
        
        private Rigidbody _rigidbody;
        private Vector2 _moveInput;
        private bool _jumpInput;

        private Vector2 MoveInput
        {
            get => _moveInput;
            set => _moveInput = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1));
        }
        
        public void Move(Vector2 moveInput) => ProcessMoveInputServerRpc(moveInput);

        public void Jump()
        {
            Debug.Log("Jump input received", this);
            ProcessJumpInputServerRpc();
        }

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void Update()
        {
            if (!IsServer) return;

            bool isOnGround = IsOnGround(transform.position + Vector3.up * groundDistance, Vector3.down,
                groundDistance * 2, groundMask);
            
            HandleMovement(isOnGround);
            
            HandleJump(isOnGround);
        }

        private void HandleMovement(bool isOnGround)
        {
            if (!IsServer) return;
            
            float speed = isOnGround ? groundSpeed : airSpeed;

            Vector3 currentPosition = transform.position;
            Vector3 newPosition = currentPosition + Vector3.forward * (_moveInput.y * Time.deltaTime * speed) +
                                  Vector3.right * (_moveInput.x * Time.deltaTime * speed);
            
            _rigidbody.MovePosition(newPosition);
            
            /*_rigidbody.AddRelativeForce(Vector3.forward * (_moveInput.y * Time.deltaTime * speed));
            _rigidbody.AddRelativeForce(Vector3.right * (_moveInput.x * Time.deltaTime * speed));*/
        }

        private void HandleJump(bool isOnGround)
        {
            if (!_jumpInput) return;
            
            _jumpInput = false;

            if (!isOnGround) return;
            
            _rigidbody.AddForce(jumpSpeed * Time.fixedDeltaTime * Vector3.up);
            
            Debug.Log("Jump!", this);
        }

        [ServerRpc]
        private void ProcessMoveInputServerRpc(Vector2 moveInput) => MoveInput = moveInput;

        [ServerRpc]
        private void ProcessJumpInputServerRpc() => _jumpInput = true;
        
        private static bool IsOnGround(Vector3 origin, Vector3 direction, float maxDistance, int groundMask) =>
            Physics.Raycast(origin, direction, maxDistance, groundMask);
    }
}