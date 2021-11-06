using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NetcodeInputTesting
{
    [DisallowMultipleComponent]
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private float speed;

        private readonly NetworkVariable<Vector3> _positionNetworkVariable =
            new NetworkVariable<Vector3>();

        private Vector2 _moveInput;

        private Vector2 MoveInput
        {
            set => _moveInput = new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1));
        }

        public void OnMove(InputAction.CallbackContext callbackContext) =>
            OnMoveInputServerRpc(callbackContext.ReadValue<Vector2>());

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (!IsServer) _positionNetworkVariable.OnValueChanged -= OnPositionNetworkValueChanged;
        }

        private void Awake()
        {
            if (!IsServer) _positionNetworkVariable.OnValueChanged += OnPositionNetworkValueChanged;
        }

        private void Update()
        {
            if (!IsServer) return;
            
            Transform myTransform = transform;
            Vector3 translation = speed * Time.deltaTime * new Vector3(_moveInput.x, 0, _moveInput.y);
            Vector3 newPosition = myTransform.position + translation;
            myTransform.position = newPosition;
            _positionNetworkVariable.Value = newPosition;
        }

        private void OnPositionNetworkValueChanged(Vector3 _, Vector3 become)
        {
            if (IsServer) return;
            
            transform.position = become;
        }

        [ServerRpc]
        private void OnMoveInputServerRpc(Vector2 moveInput) => MoveInput = moveInput;
    }
}