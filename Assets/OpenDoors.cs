using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool button_A_active = false;
    public bool button_B_active= false;
    private Animator[] animators;
    
    
    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (button_A_active)
        {
            foreach (Animator anim in animators)
            {
                anim.SetTrigger("roomComplete");
            }
            // play a sound!
        }
    }
}
