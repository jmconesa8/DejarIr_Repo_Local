using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProceduralDestroy : MonoBehaviour
{
    public Transform parent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("TileFragment")&&other!=parent.GetComponent<Collider>() &&!parent.GetComponent<DameroProceduralFragments>().laided)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        GetComponent<MeshRenderer>().material = parent.GetComponent<MeshRenderer>().material;
    }
}
