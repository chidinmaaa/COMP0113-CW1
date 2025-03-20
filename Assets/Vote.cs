using UnityEngine;
using Ubiq.Avatars;
using Ubiq.Messaging;
using Ubiq.Rooms;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System;
using System.Collections.Generic;
using AvatarFactory;

namespace AvatarFactory
{
    public class Vote : MonoBehaviour
    {
        private XRSimpleInteractable interactable;
        private VotingManager votingManager;

        private void Start()
        {
            votingManager = gameObject.GetComponent<VotingManager>();
            interactable = GetComponent<XRSimpleInteractable>(); 
            interactable.selectEntered.AddListener(Interactable_SelectEntered);  
        }

        private void Interactable_SelectEntered(SelectEnterEventArgs arg0)
        {
            votingManager.my_vote_cast = true;
        }

        private void OnDestroy()
        {
            if (interactable)
            {
                interactable.selectEntered.RemoveListener(Interactable_SelectEntered);
            }
        }
    }
}