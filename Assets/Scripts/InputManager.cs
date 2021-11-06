using GameManager;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerManager local_player;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!local_player && NetworkManager.Singleton.SpawnManager.SpawnedObjects.Count > 0)
        {
            NetworkObject playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            local_player = playerObject.GetComponent<PlayerManager>();
        }

        if ((Input.GetAxisRaw("Horizontal") != 0) || (Input.GetAxisRaw("Vertical") != 0))
        {
            local_player.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
