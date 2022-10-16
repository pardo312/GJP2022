using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Thrower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameModeTejo gameModeTejo;
    [SerializeField] UiRandomBar horizontalBar, verticalBar;
    bool flag = true;
    float value1 = 0;
    float value2 = 0;
    public bool turn;
    public void OnPointerClick(PointerEventData eventData)
    {
        flag = false;
        if (turn == true)
        {
            StartCoroutine(ThrowRoutine());
            turn = false;
        }
    }

    IEnumerator ThrowRoutine()
    {
        flag = true;
        horizontalBar.gameObject.SetActive(true);
        while (flag)
        {
            yield return null;
        }
        value1 = Mathf.Lerp(-4.5f, 4.5f, horizontalBar.GetValue());
        horizontalBar.gameObject.SetActive(false);
        flag = true;
        verticalBar.gameObject.SetActive(true);
        while (flag)
        {
            yield return null;
        }
        value2 = Mathf.Lerp(0, 40,verticalBar.GetValue());
        verticalBar.gameObject.SetActive(false);
        flag = true;
        gameModeTejo.tejo.forces = new Vector3(value1,10,10 + value2);
        gameModeTejo.Throw();
    }
}
