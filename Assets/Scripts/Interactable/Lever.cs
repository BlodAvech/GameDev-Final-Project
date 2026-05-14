using UnityEngine;

public class Lever : Interactable
{
    private bool turnOn = false;

    void Start()
    {
        PromptMessage = turnOn ? "Turn Off" : "Turn On";
    }

    protected override void Interact(GameObject interacter)
    {
        base.Interact(interacter);

        turnOn = !turnOn;
        
        Debug.Log($"Lever {turnOn}");

        PromptMessage = turnOn ? "Turn Off" : "Turn On";
    }
}
