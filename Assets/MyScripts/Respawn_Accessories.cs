using System;
using System.Linq;
using Ubiq.Messaging;
using Unity.Mathematics;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Respawn_Accessories : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject[] objects;
    private NetworkContext context;

    //private Pose[] originalPositions;
    private Quaternion[] originalRotations;
    private Vector3[] originalPositions;
    private String[] names;

    void Start()
    {
        context = NetworkScene.Register(this);

        objects = GameObject.FindGameObjectsWithTag("Accessory");

        originalPositions = new Vector3[objects.Length];
        originalRotations = new Quaternion[objects.Length];
        names = new String[objects.Length];
        int index = 0;
        foreach (GameObject obj in objects)
        {
            if (obj == null) continue;
            originalPositions[index] = obj.transform.position;
            originalRotations[index] = obj.transform.rotation;
            names[index] = obj.name;
            index++;
        }
        Debug.Log(names);


    }


    private void OnTriggerEnter(Collider other)
    {
        context.SendJson(new Message(true));
        respawn();

    }

    private void respawn()
    {
        objects = GameObject.FindGameObjectsWithTag("Accessory");
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject obj = objects[i];

            Destroy(obj);
            GameObject prefab = Resources.Load<GameObject>("Accessories/" + names[i]);
            Instantiate(prefab, originalPositions[i], originalRotations[i]);

        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var data = message.FromJson<Message>();
        if (data.held)
        {
            respawn();
        }
    }

    private struct Message
    {
        public bool held;

        public Message(bool held)
        {
            this.held = held;
        }
    }
}
