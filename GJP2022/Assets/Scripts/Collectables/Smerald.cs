using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smerald : MonoBehaviour
{
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
