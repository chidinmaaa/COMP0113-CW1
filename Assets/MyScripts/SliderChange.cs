using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class SliderChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderText = null;
    [SerializeField] private float maxSliderAmount = 2.0f;

    public void ValueChange(float value)
    {
        float localValue = value;
        sliderText.text = localValue.ToString("0.0");

        UnityEngine.Debug.Log("changed text");
    }
}
