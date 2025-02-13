using System.Collections;
using UnityEngine;

public class PaintingTrigger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public AudioSource effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spriteRenderer.enabled = true;
            if (effect != null)
            {
                effect.Play();
            }
        }
    }
}
