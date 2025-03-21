using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class OpenDoorsNetworked : MonoBehaviour
{
    NetworkContext context;
    public bool my_button_active = false;
    private bool other_button_active = false;
    private bool last_button_state = false;

    private Animator[] animators;
    private ConveyorBelt belt;

    void Start()
    {
        context = NetworkScene.Register(this);
        animators = GetComponentsInChildren<Animator>();
        var conveyorbelt = GameObject.Find("Conveyor Belt");
        belt = conveyorbelt.GetComponentInChildren<ConveyorBelt>(); 
    }

    void Update()
    {
        if (last_button_state != my_button_active)
        {
            context.SendJson(new Message()
            {
                button_active = my_button_active
            });
            last_button_state = my_button_active;
        }
       
        if (my_button_active) // & other_button_active
        {
            foreach (Animator anim in animators)
            {
                anim.SetTrigger("roomComplete");
            }
            // play a sound
            belt.UnfreezeAvatar();

            my_button_active = false;
            other_button_active = false;
            last_button_state = my_button_active;
        }
    }

    private struct Message
    {
        public bool button_active;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        other_button_active = m.button_active;
    }
}