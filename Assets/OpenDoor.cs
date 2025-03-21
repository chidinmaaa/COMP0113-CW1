using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator myAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(myAnimator != null) { 
        //    if (Input.GetKeyDown(KeyCode.O))
        //    {
        //        myAnimator.SetTrigger("roomComplete");
        //    }
        //    else if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        myAnimator.SetTrigger("roomIncomplete");
        //    }
        //}
    }
}
