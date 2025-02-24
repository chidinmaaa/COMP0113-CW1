using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;
//#if XRI_3_0_7_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
//#endif

public class GraspObject : MonoBehaviour
{
//#if XRI_3_0_7_OR_NEWER
    private NetworkContext context;
    private XRSimpleInteractable interactable;
    private RoomClient roomClient;
    private bool isHeld = false;
    private bool owner = false; // Track if this player owns the object
    private string assignedUserID; // The user ID allowed to interact with this object

    public string initialUserID; // Set this in Inspector to define ownership. Use "-1" for shared objects.

    private void Start()
    {
        context = NetworkScene.Register(this);
        interactable = GetComponent<XRSimpleInteractable>();
        roomClient = RoomClient.Find(this);

        assignedUserID = initialUserID; // Assign ownership from inspector

        interactable.selectEntered.AddListener(OnGrab);
        interactable.selectExited.AddListener(OnRelease);
    }

    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnGrab);
        interactable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs eventArgs)
    {
        string currentUserID = UserIDManager.Instance.GetUserID(roomClient.Me.uuid);

        // If the assignedUserID is "-1", allow anyone to grab it
        if (assignedUserID != "-1" && currentUserID != assignedUserID)
        {
            Debug.Log($"User {currentUserID} is NOT allowed to grab this object (Owned by {assignedUserID})");
            return;
        }

        owner = true;
        isHeld = true;
        context.SendJson(new OwnershipMessage(currentUserID, true));
    }

    private void OnRelease(SelectExitEventArgs eventArgs)
    {
        owner = false;
        isHeld = false;
        context.SendJson(new OwnershipMessage(assignedUserID, false));
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<OwnershipMessage>();

        if (!owner) // Only update if this instance is not the owner
        {
            assignedUserID = data.userID;
            isHeld = data.held;
        }
    }

    private struct OwnershipMessage
    {
        public string userID;
        public bool held;

        public OwnershipMessage(string userID, bool held)
        {
            this.userID = userID;
            this.held = held;
        }
    }
//#endif
}
