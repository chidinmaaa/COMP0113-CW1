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
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public int totalVotes = 0;
        //public Dictionary<string,int> votes = new Dictionary<string,int>();
        public Dictionary<string, int> votes = new Dictionary<string, int>();
        private AvatarManager avatarManager;

        void Start()
        {
            avatarManager = gameObject.GetComponent<AvatarManager>();
            Dictionary<IPeer, Ubiq.Avatars.Avatar>.KeyCollection players = avatarManager.playerAvatars.Keys;

            //List<string> player_ids = [];

            //for (int i = 0; i < players.count; i++)
            //    {
            //    votes.add(players[i].ToString(), 0);
            //    }
            foreach (IPeer peer in players)
            {
                Debug.Log(peer);
            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}