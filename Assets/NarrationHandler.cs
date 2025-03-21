using UnityEngine;

public class NarrationHandler : MonoBehaviour
{

    public bool in_entrance;
    public bool in_body_lab;
    public bool in_style_station;
    public bool in_accessory_studio;
    public bool in_final_room;

    private SimpleSpeechController controller;

    void Start()
    {
        controller = GetComponent<SimpleSpeechController>();
    }

    void Update()
    {
        //switch() // Simple Speech controller needs modification first
        //{
        //    case in_entrance:
        //        contoller.Invoke("SpeakWelcomeMessage", 2.0f);
        //        in_entrance = false;
        //    case in_body_lab:
        //        contoller.Invoke("SpeakBodyLab", 2.0f);
        //        in_body_lab = false;
        //    case in_style_station:
        //        contoller.Invoke("SpeakStyleStation", 2.0f);
        //        in_style_station = false;
        //    case in_accessory_studio:
        //        contoller.Invoke("SpeakAccessoryStudio", 2.0f);
        //        in_accessory_studio = false;
        //    case in_final_room:
        //        contoller.Invoke("SpeakFinalRoom", 2.0f);
        //        in_final_room = false;
        //}
    }
}
