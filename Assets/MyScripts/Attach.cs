using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (other.gameObject.name == "OurAvatar")
        {
            UnityEngine.Debug.Log("collided correctly");

            transform.SetParent(other.transform);
            //transform.localPosition = new Vector3(0, (float)0.53, 0);
            transform.localPosition = position;
            //transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.localEulerAngles = rotation;

        }
    }
}