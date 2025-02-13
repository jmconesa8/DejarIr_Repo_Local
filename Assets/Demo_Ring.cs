using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Demo_Ring : MonoBehaviour
{

    public static Action callback;


    public void PlayCallBack()
    {
        callback?.Invoke();
    }
}
