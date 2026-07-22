using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] Transform interactPoint;
    [SerializeField] private float interactRange;
    [SerializeField] LayerMask interactLayer;
    private bool canInteract = false;
    
    void Update()
    {
        canInteract = false;
        
        Collider2D[] hitInteract = Physics2D.OverlapCircleAll(interactPoint.position, interactRange, interactLayer);

        foreach (Collider2D interactInfo in hitInteract)
        {
            canInteract = true;
        }
    }

    public void Interact()
    {
        if (canInteract)
        {
            Debug.Log("Wow! you interact to this object!!");
        }
    }
}
