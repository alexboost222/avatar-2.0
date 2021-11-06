using Unity.Netcode;
using UnityEngine;

namespace MultiplayerSetup.Helpers
{
    [DisallowMultipleComponent]
    public class NotIsLocalPlayerCameraDeactivator : NetworkBehaviour
    {
        [SerializeField] private Camera cameraToDeactivate;
        
        private void Start()
        {
            if (!IsLocalPlayer && cameraToDeactivate != null) cameraToDeactivate.enabled = false;
        }
    }
}