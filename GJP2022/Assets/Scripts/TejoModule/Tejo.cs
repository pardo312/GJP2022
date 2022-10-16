using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tejo : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Vector3 forces;
    Vector3 initialPosition;
    Quaternion initialRotation;

    private void Start()
    {
        rigidbody.isKinematic = true;
        initialPosition = transform.position;
    }

    [ContextMenu("Throw")]
    public void Throw()
    {
        Restart();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(forces,ForceMode.Impulse);
    }

    public void Restart()
    {
        rigidbody.isKinematic = true;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
