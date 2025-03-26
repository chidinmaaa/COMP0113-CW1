using UnityEngine;

public class FactoryCompletionHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool voting_complete;
    private FireworksFinalRoom fireworks;
    private Animator final_door;
    private InhabitAvatar inhibit_button;

    void Start()
    {
        fireworks = GetComponentInChildren<FireworksFinalRoom>();
        final_door = GetComponentInChildren<Animator>();
        inhibit_button = GetComponentInChildren<InhabitAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (voting_complete)
        {
            fireworks.LaunchFireworks();
            fireworks.fired = true;

            final_door.SetTrigger("roomComplete");

            inhibit_button.voting_complete = true;
        }
        voting_complete = false; // avoid repeated calls
    }
}
