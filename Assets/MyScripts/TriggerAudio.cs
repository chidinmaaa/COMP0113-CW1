using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public NarrationHandler narration_handler;

    void Start(){
        narration_handler = GetComponentInParent<NarrationHandler>();
    }

    void Update()
    {}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "avatar")
        {
            var flag = transform.name;
            switch (flag)
            {
                case "in_entrance":
                    narration_handler.in_entrance = true;
                    break;
                case "in_body_lab":
                    narration_handler.in_body_lab = true;
                    break;
                case "in_style_station":
                    narration_handler.in_style_station = true;
                    break;
                case "in_accessory_studio":
                    narration_handler.in_accessory_studio = true;
                    break;
                case "in_final_room":
                    narration_handler.in_final_room = true;
                    break;
                case "on_conveyor":
                    narration_handler.on_conveyor = true;
                    break;
            }
            Destroy(transform.GetComponent<BoxCollider>()); // ensure narration only plays once
        }
    }
}