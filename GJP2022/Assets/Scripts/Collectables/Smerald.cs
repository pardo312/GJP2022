using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smerald : MonoBehaviour
{
    public bool m_shouldRotate = false;
    public float m_rotateSpeed = .5f;
    public void OnEnable()
    {
        m_shouldRotate = true;
    }

    public void Update()
    {
        if (m_shouldRotate)
            transform.Rotate(new Vector3(0, m_rotateSpeed, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Give Money To Player
            PlayerPrefs.SetFloat("smerald", PlayerPrefs.GetFloat("smerald", 0) + Random.Range(1, 4));
            Destroy(this.gameObject);
        }
    }
}
