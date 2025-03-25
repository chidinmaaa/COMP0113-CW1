using System.Collections;
using System.Diagnostics;
using Ubiq.Avatars;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Random = UnityEngine.Random;
using UnityEditor;
using System.IO;

/// <summary>
/// This class listens to the select event of an XRI interactable button. 
/// When pressed, it fetches the specified prefab and sets it as 
/// the Avatar Prefab in AvatarManager.
/// </summary>
public class InhibitAvatar : MonoBehaviour
{
    public GameObject editingAvatar;
    [Header("Original Prefab Reference")]
    public GameObject prefabReference; // Assign the original prefab

    private string prefabPath; // Stores the path of the prefab file

    private XRSimpleInteractable interactable;
    private RoomClient roomClient;
    private AvatarManager avatarManager;
    public NetworkContext context;

    public bool voting_complete;
    public bool me_ready_to_inhibit;
    public bool other_ready_to_inhibit;

    private void Start()
    {
        // Get the path of the original prefab
        context = NetworkScene.Register(this);
        prefabPath = AssetDatabase.GetAssetPath(prefabReference);

        if (string.IsNullOrEmpty(prefabPath))
        {
            UnityEngine.Debug.LogError("RuntimePrefabSaver: Original prefab path not found. Ensure this object is a prefab instance.");
        }

        UnityEngine.Debug.Log("ChangeAvatarPrefab Script Initialized");

        // Connect up the event for the XRI button.
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(Interactable_SelectEntered);

        // Get the NetworkScene and AvatarManager
        var networkScene = NetworkScene.Find(this);
        roomClient = networkScene.GetComponentInChildren<RoomClient>();
        avatarManager = networkScene.GetComponentInChildren<AvatarManager>();
    }

    private void OnDestroy()
    {
        // Cleanup the event for the XRI button
        if (interactable)
        {
            interactable.selectEntered.RemoveListener(Interactable_SelectEntered);
        }
    }


    private struct Message
    {
        public bool ready_to_inhibit;
    }

    private void Interactable_SelectEntered(SelectEnterEventArgs arg0)
    {
        if (string.IsNullOrEmpty(prefabPath))
        {
            UnityEngine.Debug.LogError("Cannot save prefab. Path is missing.");
            return;
        }

        me_ready_to_inhibit = true;
        context.SendJson(new Message()
        {
            ready_to_inhibit = true
        });

        // Apply changes from the current scene object to the prefab
        PrefabUtility.SaveAsPrefabAssetAndConnect(editingAvatar, prefabPath, InteractionMode.UserAction);
        
        if (me_ready_to_inhibit & other_ready_to_inhibit)
        {

            GameObject updatedPrefab = Resources.Load<GameObject>("MyAvatars/UbiqAvatars/NewAvatar");
            UnityEngine.Debug.Log("inhibit button got pressed");

            // enable script to ensure it can be inhabited
            //Ubiq.Avatars.Avatar avatarScript = updatedPrefab.GetComponent<Ubiq.Avatars.Avatar>();
            //// Enable the script
            //if (avatarScript != null)
            //    avatarScript.enabled = true;

            Ubiq.HeadAndHandsAvatar HeadAndHandsAvatarScript = updatedPrefab.GetComponent<Ubiq.HeadAndHandsAvatar>();
            // Enable the script
            if (HeadAndHandsAvatarScript != null)
                HeadAndHandsAvatarScript.enabled = true;

            if (updatedPrefab != null)
            {
                Vector3 scale = updatedPrefab.transform.localScale;
                scale.x = 1;
                scale.y = 1;
                scale.z = 1;
                updatedPrefab.transform.localScale = scale;

                if (voting_complete)
                {
                    avatarManager.avatarPrefab = updatedPrefab;
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning("ChangeAvatarPrefab: No prefab found");
            }
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        other_ready_to_inhibit = m.ready_to_inhibit;
    }

}
