using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathzoneController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerStateMachine>().TakeDamage(1000);
    }

}
