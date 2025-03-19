using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    public float boost;
    public Vector3 direction;
    public List<GameObject> onBelt;

    void Start(){
    }

    void Update()
    {
        for(int i = 0; i < onBelt.Count ; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().linearVelocity = speed * direction * Time.deltaTime * boost;
            //onBelt[i].GetComponent<Rigidbody>().position += direction/speed;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {  
        if (collision.gameObject.tag == "avatar")
        {
            onBelt.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    { 
        if (collision.gameObject.tag == "avatar")
        {
            onBelt.Remove(collision.gameObject);
        }
    }

    public void UnfreezeAvatar()
    {
        GameObject avatar = onBelt[0];
        avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        avatar.GetComponent<Rigidbody>().freezeRotation = true;

    }

    //in inspector can freeze z rotation on rigid body to prevent rolling  
}
