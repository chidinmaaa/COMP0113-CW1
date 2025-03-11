using UnityEngine;

public class CombineObjects : MonoBehaviour
{
    public string combineWithTag; // The tag of the object it can merge with
    public GameObject combinedPrefab; // Prefab of the new combined object

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(combineWithTag))
        {
            Combine(collision.gameObject);
        }
    }

    private void Combine(GameObject otherObject)
    {
        // Destroy both objects
        Destroy(gameObject);
        Destroy(otherObject);

        // Instantiate the combined object
        GameObject newObject = Instantiate(combinedPrefab, transform.position, Quaternion.identity);

        UnityEngine.Debug.Log("combined objects");

        //// Assign network ownership
        //var networkedObject = newObject.GetComponent<Ubiq.Messaging.NetworkedObject>();
        //if (networkedObject)
        //{
        //    networkedObject.TakeOwnership();
        //}
    }
    
}
