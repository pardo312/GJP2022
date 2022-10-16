using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject effects, Model;
    bool exploded = false;
    private void OnTriggerEnter(Collider other)
    {
        if (exploded)
            return;
        if (other.CompareTag("Tejo"))
        {
            audioSource.PlayOneShot(clip);
            effects.gameObject.SetActive(true);
            Model.gameObject.SetActive(false);
            StartCoroutine(BeHappy());
            exploded = true;
        }
    }

    IEnumerator BeHappy()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        gameObject.SetActive(true);
        Model.SetActive(true);
        effects.SetActive(false);
        exploded = false;
    }
}
