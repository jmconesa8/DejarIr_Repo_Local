using System.Collections;
using UnityEngine;

public class EyeScanInteraction : MonoBehaviour
{
    public GameObject eye;
    public Animator retinaDoorAnimator;

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
                if (eye == null)
                {
                    StartCoroutine(ScriptedSequence());
                }
            }
        }
    }

    public IEnumerator ScriptedSequence()
    {
        crBool = true;

        retinaDoorAnimator.SetTrigger("Play");

        yield return new WaitForSeconds(1);
    }
}
