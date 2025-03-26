using UnityEngine;

public class FreezeAvatar : MonoBehaviour
{
    public bool exitingRoom = false;
    GameObject avatar;

    void Start()
    {}

    void Update()
    {
        if (exitingRoom)
        {
            //avatar.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
            avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            //avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            //avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            avatar.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("freezing...");
        avatar = collision.gameObject;
        avatar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
    } // Packages/com.ucl.ubiq/Runtime/ExampleAvatars/Rocketbox/Ubiq Rocketbox Avatar.prefab

}
