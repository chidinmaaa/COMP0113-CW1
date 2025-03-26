using UnityEngine;

public class PadCollider : MonoBehaviour
{
    public bool _isPressed;
    [SerializeField]
    GameObject curtain;

    private void OnTriggerEnter(Collider col)
    {

        Debug.Log(col.gameObject);
        if (!_isPressed)
        {
            curtain.transform.position += new Vector3(0, 3, 0);
            _isPressed = true;
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isPressed)
        {
            curtain.transform.position -= new Vector3(0, 3, 0);
            _isPressed =false;
        }
        
    }

}
