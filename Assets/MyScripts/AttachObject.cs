using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Rooms;
//#if XRI_3_0_7_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Diagnostics;
using Unity.XR.CoreUtils;
//#endif

public class AttachObject : MonoBehaviour
{
    //#if XRI_3_0_7_OR_NEWER
    private NetworkContext context;
    private XRGrabInteractable interactable;
    private RoomClient roomClient;
    private bool isHeld = false;
    //private bool owner = false;
    private string assignedUserID;

    public string initialUserID; // Set in Inspector. "-1" means any user can grab it.
    [SerializeField]
    private bool isHead;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    Rigidbody rigidbody;
    Pose worldPose;
    Material mat;

    private void Start()
    {
        context = NetworkScene.Register(this);
        interactable = GetComponent<XRGrabInteractable>();
        roomClient = RoomClient.Find(this);
        assignedUserID = initialUserID;


        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
        Renderer renderer = GetComponent<Renderer>();
        mat = renderer.sharedMaterial;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        worldPose = transform.GetWorldPose();


    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Head") && isHead)
        {
            UnityEngine.Debug.Log("collided into head");
            Renderer HeadRenderer = other.gameObject.GetComponent<Renderer>();
            HeadRenderer.sharedMaterial = mat;
        } else if (other.gameObject.CompareTag("Torso") && !isHead)
        {
            UnityEngine.Debug.Log("collided into torso");
            Renderer TorsoRenderer = other.gameObject.GetComponent<Renderer>();
            TorsoRenderer.sharedMaterial = mat;
        }

        rigidbody.useGravity = false;
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        

        transform.SetWorldPose(worldPose);
        context.SendJson(new Message(false, transform.position, transform.rotation));
    }

    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnGrab);
        interactable.selectExited.RemoveListener(OnRelease);
    }

    private void Update()
    {


        if (isHeld && (transform.position != lastPosition || transform.rotation != lastRotation))
        {
            lastPosition = transform.position;
            lastRotation = transform.rotation;

            context.SendJson(new Message(true, lastPosition, lastRotation));
        }
    }

    private void OnGrab(SelectEnterEventArgs eventArgs)
    {



        isHeld = true;

        // Notify the network about ownership change
        context.SendJson(new Message(true, transform.position, transform.rotation));
        
    }

    private void OnRelease(SelectExitEventArgs eventArgs)
    {
        isHeld = false;

        // Notify network that this player released the object

        context.SendJson(new Message(false, lastPosition, lastRotation));
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();


            isHeld = data.held;
            transform.position = data.position;
            transform.rotation = data.rotation;

    }

    private struct Message
    {
        public bool held;
        public Vector3 position;
        public Quaternion rotation;

        public Message(bool held, Vector3 position, Quaternion rotation)
        {
            this.held = held;
            this.position = position;
            this.rotation = rotation;
        }
    }


}
