using System.Collections;
using System.Diagnostics;
using Ubiq.Avatars;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Random = UnityEngine.Random;

public class ChangeAvatarShape : MonoBehaviour
{
    [Header("Prefab Management")]
    [Tooltip("The list of prefabs to cycle through")]
    public GameObject[] prefabSet;

    [Tooltip("Layer where the prefab to replace is located")]
    public LayerMask prefabLayer;

    private int currentPrefabIndex = 0;
    private GameObject currentInstance;

    private XRSimpleInteractable interactable;
    private RoomClient roomClient;
    private AvatarManager avatarManager;

    private void Start()
    {
        // Connect up the event for the XRI button.
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(Interactable_SelectEntered);

        var networkScene = NetworkScene.Find(this);
        roomClient = networkScene.GetComponentInChildren<RoomClient>();
        avatarManager = networkScene.GetComponentInChildren<AvatarManager>();

        // Find the existing prefab in the scene
        FindExistingPrefab();
    }

    private void OnDestroy()
    {
        // Cleanup the event for the XRI button
        if (interactable)
        {
            interactable.selectEntered.RemoveListener(Interactable_SelectEntered);
        }
    }

    private void FindExistingPrefab()
    {
        // Find the first GameObject in the scene with the specified layer
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objects)
        {
            if (((1 << obj.layer) & prefabLayer.value) != 0)
            {
                currentInstance = obj;
                UnityEngine.Debug.Log("avatar found");
                break;
            }
        }

        if (currentInstance == null)
        {
            UnityEngine.Debug.LogWarning("ChangeShape: No prefab detected in the scene with the specified layer.");
        }
    }

    private void Interactable_SelectEntered(SelectEnterEventArgs arg0)
    {
        // The button has been pressed, cycle prefabs
        ReplacePrefab();
    }

    private void ReplacePrefab()
    {
        if (prefabSet == null || prefabSet.Length == 0)
        {
            UnityEngine.Debug.LogError("ChangeShape: No prefabs assigned!");
            return;
        }

        if (currentInstance != null)
        {
            // Store the current position and rotation before deleting
            Vector3 position = currentInstance.transform.position;
            Quaternion rotation = currentInstance.transform.rotation;

            // Destroy the current instance
            Destroy(currentInstance);
            UnityEngine.Debug.Log("destroyed existing prefab");

            // Increment prefab index (loop back to 0 if at the end)
            currentPrefabIndex = (currentPrefabIndex + 1) % prefabSet.Length;

            // Instantiate the next prefab in the list
            currentInstance = Instantiate(prefabSet[currentPrefabIndex], position, rotation);

            // Assign it to the correct layer
            currentInstance.layer = LayerMaskToLayer(prefabLayer);

            // Update the AvatarManager's avatarPrefab if applicable
            avatarManager.avatarPrefab = currentInstance;
        }
    }

    private int LayerMaskToLayer(LayerMask mask)
    {
        int layer = 0;
        int layerValue = mask.value;
        while (layerValue > 1)
        {
            layerValue >>= 1;
            layer++;
        }
        return layer;
    }
}
