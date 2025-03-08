using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
public class respawnOnCol : MonoBehaviour
{
    //Vector3 originalPos;
    Rigidbody rigidbody;
    //SphereCollider sphereCollider;
    Pose worldPose;

    private void Start()
    {
        //originalPos = transform.position;
        //Debug.Log(gameObject.name);
        //Debug.Log(originalPos);
        rigidbody = gameObject.GetComponent<Rigidbody>();
        //sphereCollider = gameObject.GetComponent<SphereCollider>();
        worldPose = transform.GetWorldPose();
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        rigidbody.linearVelocity = Vector3.zero;
<<<<<<< Updated upstream
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.useGravity = false;
=======
        //rigidbody.angularVelocity = Vector3.zero;
>>>>>>> Stashed changes
        //transform.position = originalPos;
        transform.SetWorldPose(worldPose);
    }
}
