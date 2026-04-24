using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField]
    private Transform platformTransform;

    [SerializeField]
    private PlatformMover movementScript; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(platformTransform);
            
            // Notify PlatformMover script
            if (movementScript != null)
            {
                movementScript.SetPlayerStatus(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            
            // Notify PlatformMover script
            if (movementScript != null)
            {
                movementScript.SetPlayerStatus(false);
            }
        }
    }
}