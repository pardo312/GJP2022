using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipArm : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int index;
    [SerializeField] CurrentArm currentArm;
    private void Start()
    {
        int value = PlayerPrefs.GetInt("Arm" + index.ToString(), 0);
        if (value != 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerPrefs.SetInt("SelectedArm", index);
        currentArm.UpdateSelectedArm(index);
    }
}
