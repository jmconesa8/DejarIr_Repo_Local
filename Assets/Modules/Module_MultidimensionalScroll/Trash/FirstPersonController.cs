using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public GameObject cam;

    public float speed;
    public float speedRot = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         cam.transform.Rotate((-Input.GetAxis("Mouse Y")*speedRot)*Time.deltaTime,0,0);

            //  cam.transform.rotation = cam.transform.rotation * Quaternion.AngleAxis((-Input.GetAxis("Mouse Y")*speedRot)*Time.deltaTime, cam.transform.right);


        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") );



        transform.position += (transform.forward * (Input.GetAxis("Vertical")) + transform.right * (Input.GetAxis("Horizontal")))*Time.deltaTime*speed;

    }
}
