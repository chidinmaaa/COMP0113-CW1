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
/// When pressed, it fetches a prefab from a specified layer and sets it as 
/// the Avatar Prefab in AvatarManager.
/// </summary>
public class InhibitAvatar : MonoBehaviour
{
    //[Header("Layer Settings")]
    //[Tooltip("Layer where the avatar prefab is located")]
    //public LayerMask prefabLayer;

    public GameObject editingAvatar;
    [Header("Original Prefab Reference")]
    public GameObject prefabReference; // Assign the original prefab

    private string prefabPath; // Stores the path of the prefab file

    private XRSimpleInteractable interactable;
    private RoomClient roomClient;
    private AvatarManager avatarManager;

    public bool voting_complete;

    private void Start()
    {
        // Get the path of the original prefab
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

    private void Interactable_SelectEntered(SelectEnterEventArgs arg0)
    {
        if (string.IsNullOrEmpty(prefabPath))
        {
            UnityEngine.Debug.LogError("Cannot save prefab. Path is missing.");
            return;
        }

        // Apply changes from the current scene object to the prefab
        PrefabUtility.SaveAsPrefabAssetAndConnect(editingAvatar, prefabPath, InteractionMode.UserAction);

        UnityEngine.Debug.Log("Prefab updated at: " + prefabPath);
        GameObject updatedPrefab = Resources.Load<GameObject>("MyAvatars/UbiqAvatars/NewAvatar");

        // enable script to ensure it can be inhabited
        Ubiq.Avatars.Avatar avatarScript = updatedPrefab.GetComponent<Ubiq.Avatars.Avatar>();
        // Enable the script
        if (avatarScript != null)
            avatarScript.enabled = true;
        Ubiq.HeadAndHandsAvatar HeadAndHandsAvatarScript = updatedPrefab.GetComponent<Ubiq.HeadAndHandsAvatar>();
        // Enable the script
        if (HeadAndHandsAvatarScript != null)
            HeadAndHandsAvatarScript.enabled = true;


        // Button Pressed - Fetch and Set Avatar Prefab
        UnityEngine.Debug.Log("Button Pressed: Attempting to Change Avatar Prefab");

        //GameObject newPrefab = FindPrefabInLayer();

        if (updatedPrefab != null)
        {
            //GameObject instance = Instantiate(updatedPrefab);
            Vector3 scale = updatedPrefab.transform.localScale;
            scale.x = 1;
            scale.y = 1;
            scale.z = 1;
            updatedPrefab.transform.localScale = scale;

            //avatarManager.avatarPrefab = editingAvatar;
            // TEMPORARY
            //avatarManager.avatarPrefab = updatedPrefab;
            // TEMPORARY
            if (voting_complete)
            {
                avatarManager.avatarPrefab = updatedPrefab;
                UnityEngine.Debug.Log($"Avatar prefab set to: {updatedPrefab.name}");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("ChangeAvatarPrefab: No prefab found");
        }
    }

    //private GameObject FindPrefabInLayer()
    //{
    //    GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

    //    foreach (GameObject obj in objects)
    //    {
    //        if (((1 << obj.layer) & prefabLayer.value) != 0)
    //        {
    //            return obj; // Return the first prefab found in the specified layer
    //        }
    //    }

    //    return null;
    //}
}
