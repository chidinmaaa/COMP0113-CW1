using UnityEngine;
using UnityEngine.UI;
using Ubiq.Messaging;

public class NetworkedSlider : MonoBehaviour
{
    private NetworkContext context;
    private Slider slider;
    private float lastValue;

    void Start()
    {
        context = NetworkScene.Register(this);
        slider = GetComponent<Slider>();

        if (slider != null)
        {
            // Register event listener for local slider changes
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    void OnSliderValueChanged(float value)
    {
        if (Mathf.Approximately(value, lastValue))
            return;

        lastValue = value;

        // Send updated value to all clients
        context.SendJson(new Message() { value = value });

        UnityEngine.Debug.Log("sending new slider value");
    }

    private struct Message
    {
        public float value;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        // Parse the message
        var m = message.FromJson<Message>();

        // Update slider without triggering `onValueChanged` event
        slider.SetValueWithoutNotify(m.value);

        // Update last known value
        lastValue = m.value;
    }
}
