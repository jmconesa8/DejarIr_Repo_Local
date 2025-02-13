using UnityEngine;
using System.Collections;

public class Fase2Puzzle4CanvasInteraction : MonoBehaviour
{
    // Script for Puzzle 4 in Fase 2
    [Tooltip("Canvas mesh renderer")]
    public MeshRenderer canvasMeshRenderer;
    [Tooltip("The material that goes on the canvas at the end of the puzzle")]
    public Material canvasEndMaterial;
    [Space(10)]
    [Tooltip("Objects on the canvas MUST BE RAYCAST LAYER")]
    public GameObject[] puzzleObjects1;
    public GameObject puzzleObjects1finalObject;
    public GameObject[] puzzleObjects2;
    public GameObject puzzleObjects2finalObject;
    public GameObject[] puzzleObjects3;
    public GameObject[] puzzleObjects4;
    public GameObject puzzleObjects4finalObject;
    public GameObject[] puzzleObjects5;
    public GameObject puzzleObjects5finalObject;
    [Header("Materials for the objects once enabled")]
    public Material enabledMaterial1;
    public Material enabledMaterial2;
    public Material enabledMaterial3;
    public Material enabledMaterial4;
    public Material enabledMaterial5;
    [Space(10)]
    public bool enablesCheckerboard;
    public CheckerboardTileWaySpawner checkerboard1;
    public bool enablesBonfire;
    public GameObject bonfireToEnable;
    public bool enablesPuzzle;
    public GameObject puzzleToEnable;
    public Animator elevator;
    [Tooltip("Looks at this at the end of the puzzle")]
    public Transform target;
    //private variables
    private GameObject playerCapsule;
    private GameObject playerCameraRoot;
    private PlayerIsWithinTrigger playerIsWithinTrigger;
    private MyFirstPersonController firstPersonController;
    private bool crBool;

    private void Start()
    {
        // Find the stuff
        playerCapsule = GameObject.Find("PlayerCapsule");
        playerCameraRoot = GameObject.Find("PlayerCameraRoot");
        playerIsWithinTrigger = GetComponent<PlayerIsWithinTrigger>();
        firstPersonController = playerCapsule.GetComponent<MyFirstPersonController>();
        // Disable final objects if theyre enabled
        if (puzzleObjects1finalObject.activeInHierarchy == true)
        {
            puzzleObjects1finalObject.SetActive(false);
        }
        if (puzzleObjects2finalObject.activeInHierarchy == true)
        {
            puzzleObjects2finalObject.SetActive(false);
        }
        if (puzzleObjects4finalObject.activeInHierarchy == true)
        {
            puzzleObjects4finalObject.SetActive(false);
        }
        if (puzzleObjects5finalObject.activeInHierarchy == true)
        {
            puzzleObjects5finalObject.SetActive(false);
        }
        // Disable next bonfire if its active
        if (bonfireToEnable != null && bonfireToEnable.activeInHierarchy == true)
        {
            bonfireToEnable.SetActive(false);
        }
        // Disable next puzzle if its active
        if (puzzleToEnable != null && puzzleToEnable.activeInHierarchy == true)
        {
            puzzleToEnable.SetActive(false);
        }
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
        Debug.Log("Begin Puzzle 2 Fase 2");
        // Player can't move until the end of the puzzle
        firstPersonController.canMove = false;
        yield return new WaitForSeconds(0.5f);
        // Loop for puzzleObjects1
        for (int i = 0; i < puzzleObjects1.Length; i++)
        {
            // Enable object
            puzzleObjects1[i].SetActive(true);
            // Wait until Player interacts with it
            while (firstPersonController.raycastHitSomething == false)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            puzzleObjects1[i].GetComponent<Collider>().enabled = false;
            puzzleObjects1[i].GetComponent<MeshRenderer>().material = enabledMaterial1;
            yield return new WaitForSeconds(0.1f);
        }
        puzzleObjects1finalObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        // Loop for puzzleObjects2
        for (int i = 0; i < puzzleObjects2.Length; i++)
        {
            // Enable object
            puzzleObjects2[i].SetActive(true);
            // Wait until Player interacts with it
            while (firstPersonController.raycastHitSomething == false)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            puzzleObjects2[i].GetComponent<Collider>().enabled = false;
            puzzleObjects2[i].GetComponent<MeshRenderer>().material = enabledMaterial2;
            yield return new WaitForSeconds(0.1f);
        }
        puzzleObjects2finalObject.SetActive(true);
        // Loop for puzzleObjects3
        for (int i = 0; i < puzzleObjects3.Length; i++)
        {
            // Enable object
            puzzleObjects3[i].SetActive(true);
            // Wait until Player interacts with it
            while (firstPersonController.raycastHitSomething == false)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            puzzleObjects3[i].GetComponent<Collider>().enabled = false;
            puzzleObjects3[i].GetComponent<MeshRenderer>().material = enabledMaterial3;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        // Loop for puzzleObjects4
        for (int i = 0; i < puzzleObjects4.Length; i++)
        {
            // Enable object
            puzzleObjects4[i].SetActive(true);
            // Wait until Player interacts with it
            while (firstPersonController.raycastHitSomething == false)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            puzzleObjects4[i].GetComponent<Collider>().enabled = false;
            puzzleObjects4[i].GetComponent<MeshRenderer>().material = enabledMaterial4;
            yield return new WaitForSeconds(0.1f);
        }
        puzzleObjects4finalObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        // Loop for puzzleObjects5
        for (int i = 0; i < puzzleObjects5.Length; i++)
        {
            // Enable object
            puzzleObjects5[i].SetActive(true);
            // Wait until Player interacts with it
            while (firstPersonController.raycastHitSomething == false)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            puzzleObjects5[i].GetComponent<Collider>().enabled = false;
            puzzleObjects5[i].GetComponent<MeshRenderer>().material = enabledMaterial5;
            yield return new WaitForSeconds(0.1f);
        }
        puzzleObjects5finalObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        // Deactivate puzzle objects
        for (int i = 0; i < puzzleObjects1.Length; i++)
        {
            puzzleObjects1[i].SetActive(false);
        }
        for (int i = 0; i < puzzleObjects2.Length; i++)
        {
            puzzleObjects2[i].SetActive(false);
        }
        for (int i = 0; i < puzzleObjects3.Length; i++)
        {
            puzzleObjects3[i].SetActive(false);
        }
        for (int i = 0; i < puzzleObjects4.Length; i++)
        {
            puzzleObjects4[i].SetActive(false);
        }
        for (int i = 0; i < puzzleObjects5.Length; i++)
        {
            puzzleObjects5[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        canvasMeshRenderer.material = canvasEndMaterial;
        yield return new WaitForSeconds(0.1f);
        if (target != null)
        {
            playerCameraRoot.transform.LookAt(target);
        }
        yield return new WaitForSeconds(0.1f);
        if (enablesCheckerboard == true)
        {
            checkerboard1.spawn = true;
        }
        if (enablesBonfire == true)
        {
            bonfireToEnable.SetActive(true);
        }
        if (enablesPuzzle == true)
        {
            puzzleToEnable.SetActive(true);
        }
        firstPersonController.canMove = true;
        elevator.SetBool("Open", true);
        Debug.Log("Player has completed Puzzle 4 Fase 2");
    }
}
