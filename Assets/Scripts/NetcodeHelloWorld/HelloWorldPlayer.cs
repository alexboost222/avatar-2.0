using Unity.Netcode;
using UnityEngine;

namespace NetcodeHelloWorld
{
    [DisallowMultipleComponent]
    public class HelloWorldPlayer : NetworkBehaviour
    {
        private readonly NetworkVariable<Vector3> _position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner) Move();
        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Vector3 randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                _position.Value = randomPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        [ServerRpc]
        private void SubmitPositionRequestServerRpc() => _position.Value = GetRandomPositionOnPlane();

        private static Vector3 GetRandomPositionOnPlane() =>
            new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));

        private void Update() => transform.position = _position.Value;
    }
}