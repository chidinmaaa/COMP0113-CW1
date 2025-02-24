using System.Collections.Generic;
using Ubiq.Rooms;
using Ubiq.Messaging;
using UnityEngine;
using System.Diagnostics;

public class UserIDManager : MonoBehaviour
{
    public static UserIDManager Instance { get; private set; }

    private Dictionary<string, string> userIDs = new Dictionary<string, string>(); // Maps peer UUIDs to assigned user IDs
    private RoomClient roomClient;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        roomClient = GetComponent<RoomClient>();
        roomClient.OnPeerUpdated.AddListener(OnPeerUpdated);
    }

    private void OnDestroy()
    {
        roomClient.OnPeerUpdated.RemoveListener(OnPeerUpdated);
    }

    private void OnPeerUpdated(IPeer peer)
    {
        if (!userIDs.ContainsKey(peer.uuid))
        {
            userIDs[peer.uuid] = GenerateUserID(peer.uuid);
            UnityEngine.Debug.Log($"Assigned UserID {userIDs[peer.uuid]} to Peer {peer.uuid}");
        }
    }

    public string GetUserID(string peerUUID)
    {
        return userIDs.ContainsKey(peerUUID) ? userIDs[peerUUID] : null;
    }

    private string GenerateUserID(string peerUUID)
    {
        // Generate a simple unique ID for the user (can be expanded to assign roles dynamically)
        return $"User_{userIDs.Count + 1}";
    }
}
