using Unity.Netcode;
using UnityEngine;
using Netcode.Transports.PhotonRealtime;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {

        public GameObject MainMenu;
        public GameObject MenuBackground;
        public GameObject InputManager;

        static string roomname = "XXXX";

        private void Start()
        {
            PhotonRealtimeTransport photon = NetworkManager.Singleton.GetComponent<PhotonRealtimeTransport>();

            photon.RoomName = GenerateConnectionID();
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                // StartButtons();
            }
            else
            {
                StatusLabels();

                // SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        private void Update()
        {
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                MainMenu.SetActive(true);
                MenuBackground.SetActive(true);
                InputManager.SetActive(false);
            }
            else
            {
                MainMenu.SetActive(false);
                MenuBackground.SetActive(false);
                InputManager.SetActive(true);
            }
        }

        static string GenerateConnectionID()
        {
            // Make an array of the letters we will use.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // Make a word.
            string word = "";
            for (int j = 1; j <= 4; j++)
            {
                // Pick a random number between 0 and 25
                // to select a letter from the letters array.
                int letter_num = Random.Range(0, letters.Length - 1);

                // Append the letter.
                word += letters[letter_num];
            }

            return word;
        }

        static void StartButtons()
        {
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            roomname = GUILayout.TextField(roomname, 4);
            if (GUILayout.Button("Client"))
            {
                PhotonRealtimeTransport photon = NetworkManager.Singleton.GetComponent<PhotonRealtimeTransport>();
                photon.RoomName = roomname;
                NetworkManager.Singleton.StartClient();
            }
        }

        public void SetRoomname(string code)
        {
            roomname = code;
            Debug.Log(code);
        }

        public void Host()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void Join()
        {
            PhotonRealtimeTransport photon = NetworkManager.Singleton.GetComponent<PhotonRealtimeTransport>();
            photon.RoomName = roomname;
            NetworkManager.Singleton.StartClient();
        }

        static void StatusLabels()
        {
            PhotonRealtimeTransport photon = NetworkManager.Singleton.GetComponent<PhotonRealtimeTransport>();

            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
            GUILayout.Label("Roomname: " + photon.RoomName);
        }

        static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<PlayerManager>();
            }
        }
    }
}