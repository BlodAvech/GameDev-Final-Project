using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Transform cameraHolder;

    [SerializeField]
    private TMP_Text promtText;

    [SerializeField] 
    private float distance = 3f;

    [SerializeField] 
    private LayerMask mask;

    private Interactable interactable;

    private void OnEnable()
    {
        InputManager.Controls.Player.Interact.performed += _ => Interact();
    }

    void Update()
    {
        Ray ray = new Ray(cameraHolder.transform.position , cameraHolder.transform.forward);
        RaycastHit hitinfo;
        if(Physics.Raycast(ray, out hitinfo, distance , mask))
        {
            interactable = hitinfo.collider.GetComponentInParent<Interactable>();
            if (interactable != null) promtText.text = interactable.PromptMessage;
        }
        else
        {
            interactable = null;
            promtText.text = string.Empty;
        }
    }

    public void Interact()
    {
        if(interactable != null)interactable.BaseInteract(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(cameraHolder.position , cameraHolder.position + cameraHolder.forward * distance);
    }
}
