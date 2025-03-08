using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool button_A_active = false;
    public bool button_B_active= false;
    private Animator[] animators;
<<<<<<< Updated upstream
=======
    //private ActivateBelt beltSwitch;
    private ConveyorBelt belt;
>>>>>>> Stashed changes
    
    
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
<<<<<<< Updated upstream
=======
        var conveyorbelt = GameObject.Find("Conveyor Belt");
        //beltSwitch = belt.GetComponent<ActivateBelt>();
        belt = conveyorbelt.GetComponentInChildren<ConveyorBelt>();  // get conveyor belt script
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (button_A_active)
=======
        if (button_A_active)  // & button B active
>>>>>>> Stashed changes
        {
            foreach (Animator anim in animators)
            {
                anim.SetTrigger("roomComplete");
            }
<<<<<<< Updated upstream
            // play a sound!
=======
            // play a sound

            belt.UnfreezeAvatar();
            //Debug.Log(beltSwitch.on);
            //beltSwitch.on = true;
            //Debug.Log(beltSwitch.on);

            //button_A_active = false;
            //button_B_active = false;
>>>>>>> Stashed changes
        }
    }
}
