using UnityEngine;
using System;
using System.Threading;

public class Checkpoint : MonoBehaviour
{
    private GameObject avatar;
    //private ActivateBelt beltSwitch;

    void Start(){ 
    }

    void Update(){
    }

    private void OnTriggerEnter(Collider collision){
        if (collision.gameObject.tag == "avatar"){
            avatar = collision.gameObject;
            avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            avatar.GetComponent<Rigidbody>().freezeRotation = true;
        }

    }
}
