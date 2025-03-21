using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public GameObject narration_handler;
    //public var flag;

    void Start()
    {
        //flag = gameObject.name;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        var flag = gameObject.name;
        //if (collision.gameObject.tag == "avatar")
        //{
        //    narration_handler.flag = true;
        //}

        if (collision.gameObject.tag == "avatar")
        {
            Debug.Log("COLLIDED with " + transform.name);
            narration_handler.GetType().GetProperty(flag).SetValue(narration_handler, true);
        }
    }
}
