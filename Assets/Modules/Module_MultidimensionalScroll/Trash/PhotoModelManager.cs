using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoModelManager : MonoBehaviour
{
    public GameObject photoModelPrefab;
    public GameObject photoPrefab;
    public Transform pivot;


    GameObject photo;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            photo = Instantiate(photoPrefab) as GameObject;
            photo.transform.SetParent(pivot);
            photo.transform.localRotation=Quaternion.identity;
            photo.transform.localPosition = Vector3.zero;


        }
    }
}
