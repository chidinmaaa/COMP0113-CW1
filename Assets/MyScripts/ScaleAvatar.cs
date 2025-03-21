using System.Diagnostics;
//using System.Runtime.Remoting.Contexts;
using UnityEngine;
using UnityEngine.UI;
using Ubiq.Messaging;

public class ScaleAvatar : MonoBehaviour
{
    [Header("Objects to Scale")]
    [Tooltip("First small object inside the main object")]
    public Transform head;

    [Tooltip("Second small object inside the main object")]
    public Transform torso;

    [Header("Sliders for Head")]
    public Slider object1_X_Slider;
    public Slider object1_Y_Slider;
    public Slider object1_Z_Slider;

    [Header("Sliders for Torso")]
    public Slider object2_X_Slider;
    public Slider object2_Y_Slider;
    public Slider object2_Z_Slider;

    private NetworkContext context;

    private void Start()
    {
        context = NetworkScene.Register(this);

        // Ensure objects exist
        if (head == null || torso == null)
        {
            UnityEngine.Debug.LogError("ScaleController: Objects not assigned!");
            return;
        }

        // Set slider default values based on the object's current scale
        SetSliderDefaults();

         //Add listeners to sliders for real-time scale updates
        object1_X_Slider.onValueChanged.AddListener(value => UpdateScale(1, value, 'x'));
        object1_Y_Slider.onValueChanged.AddListener(value => UpdateScale(1, value, 'y'));
        object1_Z_Slider.onValueChanged.AddListener(value => UpdateScale(1, value, 'z'));

        object2_X_Slider.onValueChanged.AddListener(value => UpdateScale(2, value, 'x'));
        object2_Y_Slider.onValueChanged.AddListener(value => UpdateScale(2, value, 'y'));
        object2_Z_Slider.onValueChanged.AddListener(value => UpdateScale(2, value, 'z'));
    }

    private void SetSliderDefaults()
    {
        object1_X_Slider.value = head.localScale.x;
        object1_Y_Slider.value = head.localScale.y;
        object1_Z_Slider.value = head.localScale.z;

        object2_X_Slider.value = torso.localScale.x;
        object2_Y_Slider.value = torso.localScale.y;
        object2_Z_Slider.value = torso.localScale.z;
    }

    private void UpdateScale(float obj_id, float value, char axis)
    {
        Vector3 newScale = new Vector3(0,0,0);
        if (obj_id == 1)
        {
            newScale = head.localScale;
        }else
        {
            newScale = torso.localScale;
        }

            switch (axis)
            {
                case 'x':
                    newScale.x = value;
                    
                    break;
                case 'y':
                    newScale.y = value;

                    break;
                case 'z':
                    newScale.z = value;

                    break;
            }

        //obj.localScale = newScale;
        if (obj_id == 1)
        {
            head.localScale = newScale;
        }
        else
        {
            torso.localScale = newScale;
        }

        context.SendJson(new Message(obj_id, value, axis));

        UnityEngine.Debug.Log("sending scaled avatar");
    }

    private struct Message
    {
        public float objID;
        public float val;
        public char ax;

        public Message(float objID, float val, char ax)
        {
            this.objID = objID;
            this.val = val;
            this.ax = ax;
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        // Parse the message
        var m = message.FromJson<Message>();

        float obj_id = m.objID;
        float value = m.val;
        char axis = m.ax;

        Vector3 newScale = new Vector3(0, 0, 0);
        if (obj_id == 1)
        {
            newScale = head.localScale;
        }
        else
        {
            newScale = torso.localScale;
        }

        switch (axis)
        {
            case 'x':
                newScale.x = value;

                break;
            case 'y':
                newScale.y = value;

                break;
            case 'z':
                newScale.z = value;

                break;
        }

        //obj.localScale = newScale;
        if (obj_id == 1)
        {
            head.localScale = newScale;
        }
        else
        {
            torso.localScale = newScale;
        }
    }
}
