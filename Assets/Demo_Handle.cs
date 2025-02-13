using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Handle : MonoBehaviour
{
    public static Demo_Handle instance;
    public GameObject avatar;   
    public GameObject camera;

    private void OnEnable()
    {
        instance = this;
    }

}
