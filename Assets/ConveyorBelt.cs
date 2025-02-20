using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed;
    public float boost;
    public Vector3 direction;
    public List<GameObject> onBelt;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < onBelt.Count ; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().linearVelocity = speed * direction * Time.deltaTime * boost;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }

    //in inspector can freeze z rotation on rigid body to prevent rolling  
}
