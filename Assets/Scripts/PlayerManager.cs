using Unity.Netcode;
using UnityEngine;

namespace GameManager
{
    public class PlayerManager : NetworkBehaviour
    {

        float horizontal;
        float vertical;

        public float moveSpeed = 10.0f;
        public float rotationSpeed = 20.0f;
        public float resistance = 0.1f;

        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        public Vector3 Velocity = new Vector3(0f, 0f, 0f);

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                //Move();
            }
        }

        public void Move(float horizontal, float vertical)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Vector3 newPosition = Position.Value + new Vector3(horizontal * Time.deltaTime * moveSpeed, vertical * Time.deltaTime * moveSpeed, 0f);
                transform.position = newPosition;
                Position.Value = newPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc(horizontal, vertical);
            }
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(float horizontal, float vertical, ServerRpcParams rpcParams = default)
        {
            Vector3 newPosition = Position.Value + new Vector3(horizontal * Time.deltaTime * moveSpeed, vertical * Time.deltaTime * moveSpeed, 0f);

            Position.Value = newPosition;
        }

        void Update()
        {
            transform.position = Position.Value;
        }
    }
}