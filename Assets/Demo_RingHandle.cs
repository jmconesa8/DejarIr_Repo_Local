using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Demo_RingHandle : MonoBehaviour
{

    public UnityEvent onStart;

    private void OnEnable()
    {
        Demo_Ring.callback += PlayEvent;
    }
    void PlayEvent()
    {
        onStart?.Invoke();
    }
}
