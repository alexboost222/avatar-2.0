using System;
using Unity.Netcode;
using UnityEngine;

namespace NetcodeHelloWorld
{
    public class NetworkVariableTest : NetworkBehaviour
    {
        [SerializeField] private float _valueChangeSpeed;

        private readonly NetworkVariable<float> _serverNetworkVariable = new NetworkVariable<float>();

        public float VariableValue => _serverNetworkVariable.Value;

        public override void OnNetworkSpawn()
        {
            if (!IsServer) return;

            _serverNetworkVariable.Value = 0.0f;
            Debug.Log($"Server's var initialized to: {_serverNetworkVariable.Value}");
        }

        private void Update()
        {
            if (!IsServer) return;

            _serverNetworkVariable.Value += _valueChangeSpeed * Time.deltaTime;
        }
    }
}