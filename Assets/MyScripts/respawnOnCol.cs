using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using System.Diagnostics;
public class respawnOnCol : MonoBehaviour
{
    //Vector3 originalPos;
   
    Rigidbody rigidbody;
    //SphereCollider sphereCollider;
    Pose worldPose;
    Material mat;

    private void Start()
    {
        //originalPos = transform.position;
        //Debug.Log(gameObject.name);
        //Debug.Log(originalPos);
        Renderer renderer = GetComponent<Renderer>();
        mat = renderer.material;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        //sphereCollider = gameObject.GetComponent<SphereCollider>();
        worldPose = transform.GetWorldPose();
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag("Head"))
        {
            UnityEngine.Debug.Log("collided into head");
            Renderer HeadRenderer = other.gameObject.GetComponent<Renderer>();
            HeadRenderer.material = mat;
        }

        rigidbody.linearVelocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.useGravity = false;

        transform.SetWorldPose(worldPose);
    }
}
