using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRandomBar : MonoBehaviour
{
    float value;
    bool orientation;
    public float velocity;
    public Transform min, max, indicator;
    private void Update()
    {
        if (orientation)
        {
            value = value += velocity * Time.deltaTime;
        }
        else
        {
            value = value -= velocity * Time.deltaTime;
        }
        value = Mathf.Clamp(value, 0, 1);
        if (value == 0 || value == 1)
            orientation = !orientation;
        if (indicator != null)
        {
            UpdateVisual();
        }
    }

    public float GetValue()
    {
        return value;
    }

    public void UpdateVisual()
    {
        indicator.transform.position = Vector3.Lerp(min.position, max.position, value);
    }
}
