using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    GameObject Platform;
    public float Speed;
    public bool IsMoving;
    void Start()
    {
        Platform = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.parent = gameObject.transform;

            StartCoroutine(WaitAndMove());
        }
    } */


    IEnumerator WaitAndMove()
    {
        IsMoving = true;
        yield return new WaitForSeconds(5f);

        Debug.Log("Moving Platform");
    }












}
