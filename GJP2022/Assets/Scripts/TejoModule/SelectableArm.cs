using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableArm : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int index;
    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<GameModeTejo>().ReclaimAwards(index);
    }
}
