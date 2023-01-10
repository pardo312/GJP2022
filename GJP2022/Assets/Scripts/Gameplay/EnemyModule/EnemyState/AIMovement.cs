using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform targetPosition;

    public void Start()
    {
        Seeker seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
    }
}
