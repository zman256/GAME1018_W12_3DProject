using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform targetPoint;
    public Transform playerTransform; 
    public float speed = 3f;
    public float boardDelay = 1.0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 currentDestination;
    
    private bool playerOnPlatform = false;
    private bool isWaiting = false;
    private bool hasReachedTarget = false;
    private bool tripActive = false; // Prevents the platform from flipping mid-trip

    void Start()
    {
        startPosition = transform.position;
        targetPosition = targetPoint.position;
        currentDestination = startPosition; // Start idle at the beginning
    }

    void FixedUpdate()
    {
        if (isWaiting) return;

        // 1. Determine where we want to go
        if (playerOnPlatform)
        {
            // If the player just got on, pick the "other" side
            if (!tripActive)
            {
                currentDestination = hasReachedTarget ? startPosition : targetPosition;
                tripActive = true;
            }
        }
        else
        {
            // If player is OFF, just go to the station closest to the player
            tripActive = false;
            float distToStart = Vector3.Distance(playerTransform.position, startPosition);
            float distToTarget = Vector3.Distance(playerTransform.position, targetPosition);
            currentDestination = (distToStart < distToTarget) ? startPosition : targetPosition;
        }

        // 2. Move toward that destination
        float distToDest = Vector3.Distance(transform.position, currentDestination);
        
        if (distToDest > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentDestination, speed * Time.fixedDeltaTime);
        }
        else
        {
            // 3. We have ARRIVED at the destination
            if (currentDestination == targetPosition) hasReachedTarget = true;
            else if (currentDestination == startPosition) hasReachedTarget = false;
            
            // If player is still on, stay parked here until they leave (tripActive remains true)
        }
    }

    public void SetPlayerStatus(bool isOn)
    {
        playerOnPlatform = isOn;
        
        if (isOn) 
        {
            // Force a wait every time the player boards
            StartCoroutine(WaitBeforeMoving());
        }
        else
        {
            // Player left, allow a new trip to be decided next time they board
            tripActive = false;
        }
    }

    private IEnumerator WaitBeforeMoving()
    {
        isWaiting = true;
        yield return new WaitForSeconds(boardDelay);
        isWaiting = false;
    }
}
