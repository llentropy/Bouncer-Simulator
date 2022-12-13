using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTouch : MonoBehaviour
{
    public UnityEvent ButtonTouched;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("hand"))
        {
            ButtonTouched.Invoke();
        }
    }
}
