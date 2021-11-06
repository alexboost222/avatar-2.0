using Unity.Netcode;
using UnityEngine;

namespace NetcodeHelloWorld
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NetworkVariableTest))]
    public class NetworkVariableTestLogger : NetworkBehaviour
    {
        [SerializeField] private float debugLogDelay;
        
        private NetworkVariableTest _networkVariableTest;
        private float _lastLogTime = float.MinValue;

        private void Awake() => _networkVariableTest = GetComponent<NetworkVariableTest>();

        private void Update()
        {
            if (!IsServer) return;

            float currentTime = Time.time;
            
            if (currentTime - _lastLogTime < debugLogDelay) return;

            _lastLogTime = currentTime;
            Debug.Log($"Server's var value is: {_networkVariableTest.VariableValue}");
        }
    }
}