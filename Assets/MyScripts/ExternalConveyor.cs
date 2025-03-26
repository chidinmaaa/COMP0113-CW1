using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExternalConveyor : MonoBehaviour
{
    public float speed;
    public float boost;
    public Vector3 direction;
    public List<GameObject> onBelt;

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < onBelt.Count; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().linearVelocity = speed * direction * Time.deltaTime * boost;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bot")
        {
            onBelt.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "bot")
        {
            Destroy(collision.gameObject); // despawn the bot
            onBelt.Remove(collision.gameObject);
        }
    }
}
