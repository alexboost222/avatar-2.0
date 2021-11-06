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
        [SerializeField] private float lookSpeed;
        
        private Rigidbody _rigidbody;
        
        private Vector2 _moveInput;
        private float _lookInput;
        private bool _jumpInput;

        private Vector2 MoveInput
        {
            get => _moveInput;
            set => _moveInput = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1));
        }
        
        public void Move(Vector2 moveInput) => ProcessMoveInputServerRpc(moveInput);

        public void Look(float lookInput) => ProcessLookInputServerRpc(lookInput);

        public void Jump() => ProcessJumpInputServerRpc();

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void Update()
        {
            if (!IsServer) return;

            bool isOnGround = IsOnGround(transform.position + Vector3.up * groundDistance, Vector3.down,
                groundDistance * 2, groundMask);
            
            HandleMovement(isOnGround);
            
            HandleJump(isOnGround);

            HandleLook();
        }

        private void HandleMovement(bool isOnGround)
        {
            if (!IsServer) return;
            
            float speed = isOnGround ? groundSpeed : airSpeed;
            Transform myTransform = transform;

            Vector3 currentPosition = myTransform.position;
            Vector3 newPosition = currentPosition + myTransform.forward * (_moveInput.y * Time.deltaTime * speed) +
                                  myTransform.right * (_moveInput.x * Time.deltaTime * speed);
            
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
        }

        private void HandleLook() => transform.Rotate(Vector3.up, _lookInput * lookSpeed * Time.deltaTime);

        [ServerRpc]
        private void ProcessMoveInputServerRpc(Vector2 moveInput) => MoveInput = moveInput;

        [ServerRpc]
        private void ProcessLookInputServerRpc(float lookInput) => _lookInput = lookInput;

        [ServerRpc]
        private void ProcessJumpInputServerRpc() => _jumpInput = true;
        
        private static bool IsOnGround(Vector3 origin, Vector3 direction, float maxDistance, int groundMask) =>
            Physics.Raycast(origin, direction, maxDistance, groundMask);
    }
}