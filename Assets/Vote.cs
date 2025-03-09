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
        private RoomClient roomClient;
        private AvatarManager avatarManager;
        private VotingManager votingManager;

        private void Start()
        {
            interactable = GetComponent<XRSimpleInteractable>(); 
            interactable.selectEntered.AddListener(Interactable_SelectEntered);

            var networkScene = NetworkScene.Find(this);
            roomClient = networkScene.GetComponentInChildren<RoomClient>();
            avatarManager = networkScene.GetComponentInChildren<AvatarManager>();  // add avatar manager attribute to store vote. also need to keep track of which users have voted already. use room client ids
            //votingManager = avatarManager.GetComponent<VotingManager>();
            votingManager = gameObject.GetComponent<VotingManager>();
        }

        private void Interactable_SelectEntered(SelectEnterEventArgs arg0)
        {
            CastVote();
        }

        private void CastVote()
        {
            votingManager.totalVotes += 1;
            //Dictionary<string, int> votes = votingManager.votes;
            //if (votes[roomClient.Me] == 0) // could make true or false
            //if (true)
            //{
            //    //    votes[roomClient.Me] = 1;
            //    votingManager.totalVotes += 1;
            //}
        }
        // Update is called once per frame
        private void Update()
        {

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