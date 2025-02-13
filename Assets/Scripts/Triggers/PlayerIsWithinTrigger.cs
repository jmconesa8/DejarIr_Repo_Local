using UnityEditor;
using UnityEngine;

public class PlayerIsWithinTrigger : MonoBehaviour
{
    // Contains Trigger voids for player interaction
    [Header("Don't touch")]
    public bool isWithinTrigger;

    [Space(10)]
    [Header("Can touch")]
    public bool changesCrosshair;
    public Crosshair crosshair;

    public void Start()
    {
        if(changesCrosshair == true)
        {
            crosshair = GameObject.Find("Crosshair").GetComponent<Crosshair>();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (changesCrosshair == true)
            {
                crosshair.ChangeCrosshairSize();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            isWithinTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isWithinTrigger = false;
        }

        if (changesCrosshair == true)
        {
            crosshair.ChangeCrosshairSize();
        }
    }
}
