using Unity.Netcode;
using UnityEngine;

namespace Helpers
{
    [DisallowMultipleComponent]
    public class CameraEnablerOnLocalPlayer : MonoBehaviour
    {
        private void Start()
        {
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost) return;

            NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().GetComponentInChildren<Camera>().enabled = false;
        }
    }
}