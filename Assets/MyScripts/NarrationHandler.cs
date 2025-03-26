using UnityEngine;

public class NarrationHandler : MonoBehaviour
{
    public bool in_entrance;
    public bool in_body_lab;
    public bool in_style_station;
    public bool in_accessory_studio;
    public bool in_final_room;
    public bool on_conveyor;

    public float pause;

    private SimpleSpeechController controller;
    private GameObject avatarManager;

    void Start()
    {
        pause = 1.0f;
        controller = GetComponent<SimpleSpeechController>();
        avatarManager = GameObject.Find("Avatar Manager");
    }

    void Update()
    {
        if (avatarManager.transform.childCount > 1)
        {
            if (in_entrance){
                controller.Speak("entrance", pause);
                in_entrance = false;
            }
            if (in_body_lab){
                in_body_lab = false;
            }
            if (in_style_station)
            {
                controller.Speak("style_station", pause);
                in_style_station = false;
            }
            if (in_accessory_studio)
            {
                controller.Speak("accessory_studio", pause);
                in_accessory_studio = false;
            }
            if (in_final_room)
            {
                controller.Speak("final_room", pause);
                in_final_room = false;
            }
            if (on_conveyor)
            {
                controller.Speak("belt", pause);
                on_conveyor = false;
            }
        }
    }
}
