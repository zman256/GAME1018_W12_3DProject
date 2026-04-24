using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    [SerializeField] private Transform platformTransform; 
    // Reference changed to your specific class name: PlatformMover
    [SerializeField] private PlatformMover movementScript; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(platformTransform);
            
            // Notify your PlatformMover script
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
            
            // Notify your PlatformMover script
            if (movementScript != null)
            {
                movementScript.SetPlayerStatus(false);
            }
        }
    }
}