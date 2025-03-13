using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ScaleAvatar : MonoBehaviour
{
    [Header("Objects to Scale")]
    [Tooltip("First small object inside the main object")]
    public Transform object1;

    [Tooltip("Second small object inside the main object")]
    public Transform object2;

    [Header("Sliders for Object 1")]
    public Slider object1_X_Slider;
    public Slider object1_Y_Slider;
    public Slider object1_Z_Slider;

    [Header("Sliders for Object 2")]
    public Slider object2_X_Slider;
    public Slider object2_Y_Slider;
    public Slider object2_Z_Slider;

    private void Start()
    {
        // Ensure objects exist
        if (object1 == null || object2 == null)
        {
            UnityEngine.Debug.LogError("ScaleController: Objects not assigned!");
            return;
        }

        // Set slider default values based on the object's current scale
        SetSliderDefaults();

        // Add listeners to sliders for real-time scale updates
        object1_X_Slider.onValueChanged.AddListener(value => UpdateScale(object1, value, 'x'));
        object1_Y_Slider.onValueChanged.AddListener(value => UpdateScale(object1, value, 'y'));
        object1_Z_Slider.onValueChanged.AddListener(value => UpdateScale(object1, value, 'z'));

        object2_X_Slider.onValueChanged.AddListener(value => UpdateScale(object2, value, 'x'));
        object2_Y_Slider.onValueChanged.AddListener(value => UpdateScale(object2, value, 'y'));
        object2_Z_Slider.onValueChanged.AddListener(value => UpdateScale(object2, value, 'z'));
    }

    private void SetSliderDefaults()
    {
        object1_X_Slider.value = object1.localScale.x;
        object1_Y_Slider.value = object1.localScale.y;
        object1_Z_Slider.value = object1.localScale.z;

        object2_X_Slider.value = object2.localScale.x;
        object2_Y_Slider.value = object2.localScale.y;
        object2_Z_Slider.value = object2.localScale.z;
    }

    private void UpdateScale(Transform obj, float value, char axis)
    {
        Vector3 newScale = obj.localScale;

        switch (axis)
        {
            case 'x':
                newScale.x = value;
                break;
            case 'y':
                newScale.y = value;
                break;
            case 'z':
                newScale.z = value;
                break;
        }

        obj.localScale = newScale;
    }
}
