using UnityEngine;
using Ubiq.Messaging;
using System.Diagnostics;

public class NetworkedMaterial : MonoBehaviour
{
    private NetworkContext context;

    [Header("All Sphere Objects")]
    public GameObject[] sphereObjects; // Assign all spheres in order via Inspector

    [Header("Target Transforms")]
    public Transform avatarHead;
    public Transform avatarTorso;

    private void Start()
    {
        context = NetworkScene.Register(this);
    }

    public void NotifyMaterialChange(string bodyPart, int materialIndex)
    {
        MaterialChangeMessage message = new MaterialChangeMessage
        {
            part = bodyPart,
            materialIndex = materialIndex
        };

        context.SendJson(message);
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<MaterialChangeMessage>();

        if (data.materialIndex >= 0 && data.materialIndex < sphereObjects.Length)
        {
            Material mat = sphereObjects[data.materialIndex].GetComponent<Renderer>().sharedMaterial;

            if (data.part == "Head" && avatarHead != null)
            {
                avatarHead.GetComponent<Renderer>().sharedMaterial = mat;

                UnityEngine.Debug.Log($"changed head by {data.materialIndex}");
            }
            else if (data.part == "Torso" && avatarTorso != null)
            {
                avatarTorso.GetComponent<Renderer>().sharedMaterial = mat;
                UnityEngine.Debug.Log($"changed torso by {data.materialIndex}");
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning($"Invalid material index: {data.materialIndex}");
        }
    }

    private struct MaterialChangeMessage
    {
        public string part;
        public int materialIndex;
    }
}
