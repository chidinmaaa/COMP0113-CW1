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
    private XRSimpleInteractable interactable;
    private OpenDoorsNetworked controller;
    private AudioSource click;

    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        interactable.hoverEntered.AddListener(Interactable_HoverEntered);
        interactable.hoverExited.AddListener(Interactable_HoverExited);

        click = GetComponent<AudioSource>();
        controller = GetComponentInParent<OpenDoorsNetworked>();
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
        controller.my_button_active = true;
    }

    private void Interactable_HoverExited(HoverExitEventArgs arg0)
    {
        controller.my_button_active = false;
    }
}