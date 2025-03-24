using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Ubiq.Messaging;
using Ubiq.Rooms;

public class AttachAccessory : MonoBehaviour
{
    private NetworkContext context;
    private XRGrabInteractable interactable;
    private RoomClient roomClient;
    private string currentUserID;

    public string assignedUserID;

    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Vector3 rotation;

    private void Start()
    {
        context = NetworkScene.Register(this);
        interactable = GetComponent<XRGrabInteractable>();
        roomClient = RoomClient.Find(this);
        currentUserID = UserIDManager.Instance.GetUserID(roomClient.Me.uuid);

        assignedUserID = assignedUserID == "" ? "-1" : assignedUserID;
    }

    private void OnCollisionEnter(Collision other)
    {
        UnityEngine.Debug.Log(other.gameObject.name);

        if (other.gameObject.name == "OurAvatar")
        {
            UnityEngine.Debug.Log("collided correctly");

            transform.SetParent(other.transform);
            transform.localPosition = position;
            transform.localEulerAngles = rotation;

            Transform bodyOfAvatar = other.gameObject.transform.Find("Body");
            if (bodyOfAvatar != null)
            {
                Transform headOfAvatar = bodyOfAvatar.Find("Floating_Head");
                if (headOfAvatar != null)
                {
                    transform.SetParent(headOfAvatar);
                }
                else
                {
                    UnityEngine.Debug.Log("Could not find head of the avatar");
                }
            }
            else
            {
                UnityEngine.Debug.Log("Could not find body of the avatar");
            }

            Destroy(GetComponent<XRGrabInteractable>());
            Destroy(GetComponent<Rigidbody>());

            if (assignedUserID == currentUserID || assignedUserID == "-1")
            {
                context.SendJson(new Message(transform.position, transform.rotation));
            }
        }
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
