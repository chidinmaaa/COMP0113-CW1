using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Respawn_Accessories : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject[] objects;
    private Pose[] originalPositions;

    void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("Accessory");
        originalPositions = new Pose[objects.Length];
        int index = 0;
        foreach (GameObject obj in objects)
        {
            if (obj == null) continue;
            originalPositions[index] = obj.transform.GetWorldPose();
            index++;
        }

        
    }


    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject obj = objects[i];
            if (obj != null)
            {
                obj.transform.SetParent(null);
                obj.transform.SetWorldPose(originalPositions[i]);

            }
        }
    }
}
