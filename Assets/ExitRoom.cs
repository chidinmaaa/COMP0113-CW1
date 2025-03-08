using System.Collections;
using Ubiq.Avatars;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Random = UnityEngine.Random;


public class ExitRoom : MonoBehaviour
{
    public GameObject prefab;
    private XRSimpleInteractable interactable;
    private RoomClient roomClient;
    private AvatarManager avatarManager;
    private OpenDoors controller;
    private AudioSource click;
    private string button_name;

    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.hoverEntered.AddListener(Interactable_HoverEntered);
        interactable.hoverExited.AddListener(Interactable_HoverExited);

        var networkScene = NetworkScene.Find(this);
        roomClient = networkScene.GetComponentInChildren<RoomClient>();
        avatarManager = networkScene.GetComponentInChildren<AvatarManager>();
   
        click = GetComponent<AudioSource>();
        controller = GetComponentInParent<OpenDoors>();
        button_name = transform.name;
<<<<<<< Updated upstream
        Debug.Log(button_name);
=======
>>>>>>> Stashed changes
    }

    private void OnDestroy()
    {
        if (interactable)
        {
            interactable.hoverEntered.RemoveListener(Interactable_HoverEntered);
            interactable.hoverExited.RemoveListener(Interactable_HoverExited);
        }
    }

    private void Interactable_HoverEntered(HoverEnterEventArgs arg0)
    {
        click.Play(0);
<<<<<<< Updated upstream
        if (button_name == "Button A")
        {
            controller.button_A_active = true;
        }
        else
        {
=======
        if (button_name == "Button A"){
            controller.button_A_active = true;
        }
        else {
>>>>>>> Stashed changes
            controller.button_B_active = true;
        }
    }

    private void Interactable_HoverExited(HoverExitEventArgs arg0)
    {
<<<<<<< Updated upstream
        if (button_name == "Button A")
        {
            controller.button_A_active = false;
        }
        else
        {
=======
        if (button_name == "Button A"){
            controller.button_A_active = false;
        }
        else {
>>>>>>> Stashed changes
            controller.button_B_active = false;
        }
    }
}