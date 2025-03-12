using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Rooms;
//#if XRI_3_0_7_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Diagnostics;
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

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void Start()
    {
        context = NetworkScene.Register(this);
        interactable = GetComponent<XRGrabInteractable>();
        roomClient = RoomClient.Find(this);
        assignedUserID = initialUserID;

        //if (roomClient == null)
        //{
        //    UnityEngine.Debug.Log($"[GraspObject] room client is null");
        //}
        //else
        //{
        //    UnityEngine.Debug.Log($"[GraspObject] room client is NOT null");
        //}

        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);

        UnityEngine.Debug.Log("[GraspObject] assigned ID = " + assignedUserID);

        UnityEngine.Debug.Log("[GraspObject] peer uuid = " + roomClient.Me.uuid);
    }

    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnGrab);
        interactable.selectExited.RemoveListener(OnRelease);
    }

    private void Update()
    {

        //string currentUserID = UserIDManager.Instance.GetUserID(roomClient.Me.uuid);
        // If this player is the owner, send updates if the object moves
        if (isHeld && (transform.position != lastPosition || transform.rotation != lastRotation))
        {
            lastPosition = transform.position;
            lastRotation = transform.rotation;

            //context.SendJson(new Message(currentUserID, true, lastPosition, lastRotation));
            context.SendJson(new Message(true, lastPosition, lastRotation));
        }
    }

    private void OnGrab(SelectEnterEventArgs eventArgs)
    {
        //string currentUserID = UserIDManager.Instance.GetUserID(roomClient.Me.uuid);

        // Allow only the assigned user or any user if "-1"

        //if (assignedUserID != "-1" && currentUserID != assignedUserID)
        //{
        //    UnityEngine.Debug.Log($"User {currentUserID} is NOT allowed to grab this object (Owned by {assignedUserID})");

        //    // trying to not allow the object to move
        //    transform.position = lastPosition;
        //    transform.rotation = lastRotation;
        //    return;
        //}


        //owner = true;
        isHeld = true;

        // Notify the network about ownership change
        //context.SendJson(new Message(currentUserID, true, transform.position, transform.rotation));
        context.SendJson(new Message(true, transform.position, transform.rotation));
        UnityEngine.Debug.Log("heyyyyy");
    }

    private void OnRelease(SelectExitEventArgs eventArgs)
    {
        //owner = false;
        isHeld = false;

        // Notify network that this player released the object
        //context.SendJson(new Message(assignedUserID, false, lastPosition, lastRotation));
        context.SendJson(new Message(false, lastPosition, lastRotation));
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();

        //if (!owner) // Only update if not the owner
        //{
            //assignedUserID = data.userID;
            isHeld = data.held;
            transform.position = data.position;
            transform.rotation = data.rotation;
        //}

        /*
        if (message.Tag == "ownership")
        {
            var data = message.FromJson<OwnershipMessage>();

            if (!owner) // Only update if not the owner
            {
                assignedUserID = data.userID;
                isHeld = data.held;
            }
        }
        else if (message.Tag == "position")
        {
            var data = message.FromJson<ObjectUpdateMessage>();

            if (!owner) // Only non-owners should update
            {
                transform.position = data.position;
                transform.rotation = data.rotation;
            }
        }
        */
    }

    private struct Message
    {
        //public string userID;
        public bool held;
        public Vector3 position;
        public Quaternion rotation;

        //public Message(string userID, bool held, Vector3 position, Quaternion rotation)
        public Message(bool held, Vector3 position, Quaternion rotation)
        {
            //this.userID = userID;
            this.held = held;
            this.position = position;
            this.rotation = rotation;
        }
    }

    /*
    private struct ObjectUpdateMessage
    {
        public Vector3 position;
        public Quaternion rotation;

        public ObjectUpdateMessage(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
    */
    //#endif
}
