using System;
using System.Linq;
using Unity.Mathematics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Respawn_Accessories : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject[] objects;
    //private Pose[] originalPositions;
    private Quaternion[] originalRotations;
    private Vector3[] originalPositions;
    private String[] names;

    void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("Accessory");
        //GameObject[] prefabs = Resources.LoadAll<GameObject>("Accessories");

        //originalPositions = new Pose[objects.Length];
        originalPositions = new Vector3[objects.Length];
        originalRotations = new Quaternion[objects.Length];
        names = new String[objects.Length];
        int index = 0;
        foreach (GameObject obj in objects)
        {
            if (obj == null) continue;
            //originalPositions[index] = obj.transform.GetWorldPose();
            originalPositions[index] = obj.transform.position;
            originalRotations[index] = obj.transform.rotation;
            names[index] = obj.name;
            index++;
        }
        Debug.Log(names);


    }


    private void OnTriggerEnter(Collider other)
    {
        objects = GameObject.FindGameObjectsWithTag("Accessory");
        //Debug.Log("Accesory collider " + other.gameObject.name);
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject obj = objects[i];
            //Vector3 pos = obj.transform.position;
            //quaternion quaternion = obj.transform.rotation;
            //String name = obj.name;
            Destroy(obj);
            GameObject prefab = Resources.Load<GameObject>("Accessories/" + names[i]);
            Instantiate(prefab, originalPositions[i], originalRotations[i]);
            //if (obj != null)
            //{
            //    obj.transform.SetParent(null);
            //    obj.transform.SetWorldPose(originalPositions[i]);

            //    if (obj.GetComponent<Rigidbody>() == null)
            //    {
            //        Rigidbody rb = obj.AddComponent<Rigidbody>();
            //        rb.useGravity = false;
            //    }

            //    if (obj.GetComponent<XRGrabInteractable>() == null)
            //    {
            //        obj.AddComponent<XRGrabInteractable>();
            //    }

            //}
        }

        //Debug.Log(prefabs.Length);

        //foreach (GameObject obj in prefabs) {
        //    Debug.Log(obj.name);
        //    Instantiate(obj);
        //}
    }
}
