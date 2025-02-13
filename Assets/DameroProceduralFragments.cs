using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameroProceduralFragments : MonoBehaviour
{
    public bool laided;
        
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate()
    {

        laided = true;
    }                    public void DeActivate()
    {
        laided = false;
    }
}
