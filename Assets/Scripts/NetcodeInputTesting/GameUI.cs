using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace NetcodeInputTesting
{
    [DisallowMultipleComponent]
    public class GameUI : MonoBehaviour
    {
        private const string HostName = "Host";
        private const string ServerName = "Server";
        private const string ClientName = "Client";
        
        [SerializeField] private Text modeText;

        private void Update()
        {
            string mode = NetworkManager.Singleton.IsHost ? HostName :
                NetworkManager.Singleton.IsServer ? ServerName : ClientName;

            modeText.text = $"Mode: {mode}";
        }
    }
}