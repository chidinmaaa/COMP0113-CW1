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
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        //sphereCollider = gameObject.GetComponent<SphereCollider>();
        worldPose = transform.GetWorldPose();
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.useGravity = false;
        //rigidbody.angularVelocity = Vector3.zero;
        //transform.position = originalPos;
        transform.SetWorldPose(worldPose);
    }
}
