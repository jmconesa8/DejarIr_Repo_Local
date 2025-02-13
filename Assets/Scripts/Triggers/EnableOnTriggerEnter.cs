using UnityEngine;

public class EnableOnTriggerEnter : MonoBehaviour
{
    public GameObject enabledObject;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enabledObject.SetActive(true);
        }
    }
}
