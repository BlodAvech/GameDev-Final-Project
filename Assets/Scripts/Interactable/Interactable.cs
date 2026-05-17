using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private string promptMessage;
    public string PromptMessage
    {
        get => promptMessage;
        protected set => promptMessage = value;
    }

    private bool canInteract = true;
    public bool CanInteract
    {
        get => canInteract;
        protected set => canInteract = value;
    }
    public void BaseInteract(GameObject interacter)
    {
        Interact(interacter);
    }

    protected virtual void Interact(GameObject interacter)
    {

    }

}
