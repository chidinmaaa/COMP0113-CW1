using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Attach : MonoBehaviour
{
    private LayerMask accessoryLayer;
    private int playerLayer;

    private string tagToAdd;
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Vector3 rotation;

    private void OnCollisionEnter(Collision other)
    {
        UnityEngine.Debug.Log(other.gameObject.name);
        // OurAvatar, Floating_Head, Floating_Torso_A

        if (other.gameObject.name == "OurAvatar")
        {
            UnityEngine.Debug.Log("collided correctly");

            transform.SetParent(other.transform);
            //transform.localPosition = new Vector3(0, (float)0.53, 0);
            transform.localPosition = position;
            //transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.localEulerAngles = rotation;

            Transform bodyOfAvatar = other.gameObject.transform.Find("Body");

            if (bodyOfAvatar != null)
            {
                Transform headOfAvatar = bodyOfAvatar.Find("Floating_Head");
                if (headOfAvatar != null)
                {
                    transform.SetParent(headOfAvatar);
                }
                else
                {
                    UnityEngine.Debug.Log("Could not find head of the avatar");
                }
            
            }
            else
            {
                UnityEngine.Debug.Log("Could not find body of the avatar");
            }

            //Destroy(transform.Find("XRGrabInteractable"));
            Destroy(GetComponent<XRGrabInteractable>());
            Destroy(gameObject.GetComponent<Rigidbody>());

        }
    }
}