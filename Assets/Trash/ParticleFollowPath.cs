using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPath : MonoBehaviour
{
    public string pathName;
    public float time;

    void Start()
    {
        transform.GetComponent<ParticleSystem>().Stop();
         foreach (Transform t in transform)
        {
            t.GetComponent<ParticleSystem>().Stop();
        }
    }

    public void PlayParticles()
    {
        transform.GetComponent<ParticleSystem>().Play();

        foreach (Transform t in transform)
        {
            t.GetComponent<ParticleSystem>().Play();
        }
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "easetype", iTween.EaseType.easeInOutSine, "time", time));

    }

    public void StopParticles()
    {
        transform.GetComponent<ParticleSystem>().Stop();

        foreach (Transform t in transform)
        {
            t.GetComponent<ParticleSystem>().Stop();
        }
    }


}
