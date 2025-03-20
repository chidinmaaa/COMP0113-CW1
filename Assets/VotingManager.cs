using UnityEngine;
using System;
using System.Collections.Generic;
using Ubiq.Avatars;
using Ubiq.Rooms;
using Ubiq.Messaging;
using Ubiq.Dictionaries;
using Ubiq.Spawning;
using Ubiq.Voip;
using UnityEngine;
using UnityEngine.Events;

namespace AvatarFactory
{
    public class VotingManager : MonoBehaviour
    {
        private NetworkContext context;
        private FactoryCompletionHandler completion_handler;

        public bool my_vote_cast = false;
        private bool other_vote_cast = false;

        void Start()
        {
            context = NetworkScene.Register(this);
            completion_handler = GetComponentInParent<FactoryCompletionHandler>();   
        }

        void Update()
        {
            if (my_vote_cast)
            {
                context.SendJson(new Message()
                {
                    vote_cast = true
                });
            }
            if (my_vote_cast) // & other vote cast
            {
                completion_handler.voting_complete = true;
            }
        }

        private struct Message
        {
            public bool vote_cast;
        }

        public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
        {
            var m = message.FromJson<Message>();
            other_vote_cast = m.vote_cast;
        }
    }
}