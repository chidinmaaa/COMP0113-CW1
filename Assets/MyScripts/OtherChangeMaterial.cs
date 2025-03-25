using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class OtherChangeMaterial : MonoBehaviour
{
    private NetworkContext context;
    private Rigidbody rb;
    private Renderer sphereRenderer;
    public bool isHead;
    public Transform avatarHead;
    public Transform avatarTorso;

    // Store initial pose so we can reset
    private Vector3 startPosition;
    private Quaternion startRotation;
    Material mat;

    private void Start()
    {
        context = NetworkScene.Register(this);
        // Cache references
        rb = GetComponent<Rigidbody>();
        sphereRenderer = GetComponent<Renderer>();
        mat = sphereRenderer.sharedMaterial;

        // Record starting transform
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool head = false;
        bool torso = false;

        // If we collide with something tagged "Head"
        if (other.gameObject.CompareTag("Head") && isHead)
        {
            UnityEngine.Debug.Log("Collided with Head");
            Renderer headRenderer = other.gameObject.GetComponent<Renderer>();
            if (headRenderer != null)
            {
                // Transfer sphere's sharedMaterial to the head object
                headRenderer.sharedMaterial = mat;
                head = true;
                torso = false;
            }
        }
        // If we collide with something tagged "Torso"
        else if (other.gameObject.CompareTag("Torso") && !isHead)
        {
            UnityEngine.Debug.Log("Collided with Torso");
            Renderer torsoRenderer = other.gameObject.GetComponent<Renderer>();
            if (torsoRenderer != null)
            {
                // Transfer sphere's sharedMaterial to the torso object
                torsoRenderer.sharedMaterial = mat;
                torso = true;
                head = false;
            }
        }

        // Once the material is transferred, reset the sphere
        ResetSphere();

        // head = 1; torso = 2; neither = 3

        if (head)
        {
            context.SendJson(new Message(transform.position, transform.rotation, mat, 1));
        }
        else if (torso)
        {
            context.SendJson(new Message(transform.position, transform.rotation, mat, 2));
        }
        else
        {
            context.SendJson(new Message(transform.position, transform.rotation, mat, 3));
        }

        //context.SendJson(new Message(transform.position, transform.rotation));
    }

    private void ResetSphere()
    {
        // Move back to our initial position & rotation
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Clear any leftover velocity so it doesn't continue flying
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();
        transform.position = data.position;
        transform.rotation = data.rotation;
        float id = data.objID;

        if (id == 1)
        {
            // head
            Renderer headRenderer = avatarHead.gameObject.GetComponent<Renderer>();
            if (headRenderer != null)
            {
                // Transfer sphere's sharedMaterial to the torso object
                headRenderer.sharedMaterial = data.material;
            }
        } else if (id == 2)
        {
            Renderer torsoRenderer = avatarTorso.gameObject.GetComponent<Renderer>();
            if (torsoRenderer != null)
            {
                // Transfer sphere's sharedMaterial to the torso object
                torsoRenderer.sharedMaterial = data.material;
            }
        }
    }

    private struct Message
    {
        public Vector3 position;
        public Quaternion rotation;
        public Material material;
        public float objID;

        public Message(Vector3 position, Quaternion rotation, Material material, float objID)
        {
            this.position = position;
            this.rotation = rotation;
            this.material = material;
            this.objID = objID;
        }
    }
}
