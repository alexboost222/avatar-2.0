using Unity.Netcode;
using UnityEngine;

namespace MultiplayerSetup.Controllers
{
    [DisallowMultipleComponent]
    public class CameraLookController : NetworkBehaviour
    {
        [SerializeField] private float lookSpeed;

        private readonly NetworkVariable<Quaternion> _rotationNetworkVariable = new NetworkVariable<Quaternion>();
        
        private float _lookInput;
        
        public void Look(float lookInput) => ProcessLookInputServerRpc(lookInput);

        private void Update()
        {
            Transform myTransform = transform;
            
            if (IsServer)
            {
                myTransform.Rotate(Vector3.right, _lookInput * lookSpeed * Time.deltaTime);
                Debug.Log($"Writing server rotation {myTransform.rotation}");
                _rotationNetworkVariable.Value = myTransform.rotation;
            }
            else
            {
                Debug.Log($"Applying server rotation {_rotationNetworkVariable.Value}");
                myTransform.rotation = _rotationNetworkVariable.Value;
            }
        }

        [ServerRpc]
        private void ProcessLookInputServerRpc(float lookInput) => _lookInput = lookInput;
    }
}