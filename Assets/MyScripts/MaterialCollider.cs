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
public class MaterialCollider : MonoBehaviour
{
    private NetworkContext context;
    private Rigidbody rb;
    private Renderer sphereRenderer;
    public bool isHead;
    public int ID;

    // Store initial pose so we can reset
    private Vector3 startPosition;
    private Quaternion startRotation;
    Material mat;
    NetworkedMaterial notifier;

    private void Start()
    {
        context = NetworkScene.Register(this);
        // Cache references
        rb = GetComponent<Rigidbody>();
        sphereRenderer = GetComponent<Renderer>();
        mat = sphereRenderer.sharedMaterial;
        notifier = FindObjectOfType<NetworkedMaterial>();

        // Record starting transform
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If we collide with something tagged "Head"
        if (other.gameObject.CompareTag("Head") && isHead)
        {
            
            Renderer headRenderer = other.gameObject.GetComponent<Renderer>();
            if (headRenderer != null)
            {
                // Transfer sphere's sharedMaterial to the head object
                headRenderer.sharedMaterial = mat;

                notifier?.NotifyMaterialChange("Head", ID);
            }

            context.SendJson(new Message(transform.position, transform.rotation));
        }
        // If we collide with something tagged "Torso"
        else if (other.gameObject.CompareTag("Torso") && !isHead)
        {
            
            Renderer torsoRenderer = other.gameObject.GetComponent<Renderer>();
            if (torsoRenderer != null)
            {
                // Transfer sphere's sharedMaterial to the torso object
                torsoRenderer.sharedMaterial = mat;

                notifier?.NotifyMaterialChange("Torso", ID);
            }

            context.SendJson(new Message(transform.position, transform.rotation));
        } else if (other.gameObject.CompareTag("Curtain"))
        {
            ResetSphere();
            return;
        }

            // Once the material is transferred, reset the sphere
            ResetSphere();

        
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
    }

    private struct Message
    {
        public Vector3 position;
        public Quaternion rotation;

        public Message(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
