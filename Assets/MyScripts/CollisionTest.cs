using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.gameObject.tag == "texture")
        {
            UnityEngine.Debug.Log("colliding");
        }
    }
}
