using System.Collections;
using UnityEngine;

public class ClosetInteraction : MonoBehaviour
{
    // Script for Closet in Fase 1
    //private variables
    private GameObject playerCapsule;
    private GameObject playerCameraRoot;
    private PlayerIsWithinTrigger playerIsWithinTrigger;
    private MyFirstPersonController firstPersonController;
    private bool crBool;

    private void Start()
    {
        playerCapsule = GameObject.Find("PlayerCapsule");
        playerCameraRoot = GameObject.Find("PlayerCameraRoot");
        playerIsWithinTrigger = GetComponent<PlayerIsWithinTrigger>();
        firstPersonController = playerCapsule.GetComponent<MyFirstPersonController>();
    }

    private void Update()
    {
        if (playerIsWithinTrigger.isWithinTrigger == true)
        {
            if (!crBool && firstPersonController.interacts == true)
            {
                StartCoroutine(ScriptedSequence());
            }
        }
    }

    public IEnumerator ScriptedSequence()
    {
        crBool = true;
        gameObject.GetComponent<Animator>().SetTrigger("Play");
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
