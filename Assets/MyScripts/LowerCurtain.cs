using UnityEngine;

public class LowerCurtain : MonoBehaviour
{
    private GameObject curtain;
    public float move_dist;
    public Vector3 target;
    void Start()
    {
        curtain = GameObject.Find("Glass_Cylinder");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "avatar"){
            //curtain.transform.position -= new Vector3(0, move_dist, 0);
            curtain.transform.localPosition = target;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "avatar")
        {
            curtain.transform.position += new Vector3(0, move_dist, 0);
        }
    }

    void Update()
    {

    }
}
