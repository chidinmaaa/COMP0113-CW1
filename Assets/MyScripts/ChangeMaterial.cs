using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class ChangeMaterial : MonoBehaviour
{
    private NetworkContext context;
    private Rigidbody rb;
    private Renderer sphereRenderer;

    // Store initial pose so we can reset
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        context = NetworkScene.Register(this);
        // Cache references
        rb = GetComponent<Rigidbody>();
        sphereRenderer = GetComponent<Renderer>();

        // Record starting transform
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If we collide with something tagged "Head"
        if (collision.gameObject.CompareTag("Head"))
        {
            UnityEngine.Debug.Log("Collided with Head");
            Renderer headRenderer = collision.gameObject.GetComponent<Renderer>();
            if (headRenderer != null)
            {
                // Transfer sphere's sharedMaterial to the head object
                headRenderer.sharedMaterial = sphereRenderer.sharedMaterial;
            }
        }
        // If we collide with something tagged "Torso"
        else if (collision.gameObject.CompareTag("Torso"))
        {
            UnityEngine.Debug.Log("Collided with Torso");
            Renderer torsoRenderer = collision.gameObject.GetComponent<Renderer>();
            if (torsoRenderer != null)
            {
                // Transfer sphere's sharedMaterial to the torso object
                torsoRenderer.sharedMaterial = sphereRenderer.sharedMaterial;
            }
        }

        // Once the material is transferred, reset the sphere
        ResetSphere();

        context.SendJson(new Message(transform.position, transform.rotation));
    }

    private void ResetSphere()
    {
        // Move back to our initial position & rotation
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Clear any leftover velocity so it doesn't continue flying
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
