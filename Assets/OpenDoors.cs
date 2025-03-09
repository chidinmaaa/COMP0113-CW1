using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool button_A_active = false;
    public bool button_B_active= false;
    private Animator[] animators;
    //private ActivateBelt beltSwitch;
    private ConveyorBelt belt;
    
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        var conveyorbelt = GameObject.Find("Conveyor Belt");
        //beltSwitch = belt.GetComponent<ActivateBelt>();
        belt = conveyorbelt.GetComponentInChildren<ConveyorBelt>();  // get conveyor belt script
    }

    // Update is called once per frame
    void Update()
    {
        if (button_A_active)  // & button B active
        {
            foreach (Animator anim in animators)
            {
                anim.SetTrigger("roomComplete");
            }
            // play a sound

            belt.UnfreezeAvatar();
            //Debug.Log(beltSwitch.on);
            //beltSwitch.on = true;
            //Debug.Log(beltSwitch.on);

            //button_A_active = false;
            //button_B_active = false;
        }
    }
}
