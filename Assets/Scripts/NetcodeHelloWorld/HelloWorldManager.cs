using Unity.Netcode;
using UnityEngine;

namespace NetcodeHelloWorld
{
    [DisallowMultipleComponent]
    public class HelloWorldManager : MonoBehaviour
    {
        private const string HostName = "Host";
        private const string ServerName = "Server";
        private const string ClientName = "Client";

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));

            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        private static void StartButtons()
        {
            if (GUILayout.Button(HostName)) NetworkManager.Singleton.StartHost();
            if (GUILayout.Button(ClientName)) NetworkManager.Singleton.StartClient();
            if (GUILayout.Button(ServerName)) NetworkManager.Singleton.StartServer();
        }

        private static void StatusLabels()
        {
            string mode = NetworkManager.Singleton.IsHost ? HostName :
                NetworkManager.Singleton.IsServer ? ServerName : ClientName;

            GUILayout.Label("Transport: " +
                            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        private static void SubmitNewPosition()
        {
            const string moveName = "Move";
            const string requestPositionChangeName = "Request Position Change";

            if (!GUILayout.Button(NetworkManager.Singleton.IsServer ? moveName : requestPositionChangeName)) return;

            NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            HelloWorldPlayer player = playerObject.GetComponent<HelloWorldPlayer>();
            player.Move();
        }
    }
}