using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentArm : MonoBehaviour
{
    public List<GameObject> currentArms = new List<GameObject> ();

    private void Start()
    {
        int value = PlayerPrefs.GetInt("SelectedArm",0);
        UpdateSelectedArm(value);
    }

    public void UpdateSelectedArm(int value)
    {
        for (int i = 0; i < currentArms.Count; i++)
        {
            currentArms[i].SetActive(false);
        }
        currentArms[value].SetActive(true);
    }
}
