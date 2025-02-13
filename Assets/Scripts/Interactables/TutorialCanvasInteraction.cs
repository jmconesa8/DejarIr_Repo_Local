using System.Collections;
using UnityEngine;

public class TutorialCanvasInteraction : MonoBehaviour
{
    // Script for Tutorial Puzzle
    [Tooltip("Canvas mesh renderer")]
    public MeshRenderer canvasMeshRenderer;
    [Tooltip("The material that goes on the canvas at the end of the puzzle")]
    public Material canvasEndMaterial;
    [Tooltip("Squares on the canvas")]
    public GameObject[] puzzleObjects;
    [Tooltip("Checkerboard tiles at the end of the puzzle")]
    public GameObject[] tiles;
    [Tooltip("The doors man")]
    public GameObject[] doors;
    [Tooltip("Leaves that change color on target[0]")]
    public GameObject[] leaves0;
    [Tooltip("Leaves that change color on target[1]")]
    public GameObject[] leaves1;
    [Tooltip("Targets for transform.lookAt")]
    public Transform[] targets;

    [Space(10)]
    [Header("Materials for the squares")]
    public Material enabledMaterial;
    
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

        Debug.Log("Begin Tutorial puzzle");
        // Player can't move until the end of the puzzle
        firstPersonController.canMove = false;

        // First Box in canvas
        puzzleObjects[0].SetActive(true);
        // Wait until Player interacts with it
        while(firstPersonController.raycastHitSomething == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        puzzleObjects[0].GetComponent<BoxCollider>().enabled = false;
        puzzleObjects[0].GetComponent<MeshRenderer>().material = enabledMaterial;
        // Look at the target
        playerCameraRoot.transform.LookAt(targets[0]);
        // Change the color of the leaves
        for(int i = 0; i < leaves0.Length; i++)
        {
            leaves0[i].GetComponent<MeshRenderer>().material.color = Color.Lerp(leaves0[i].GetComponent<MeshRenderer>().material.color, Color.gray, 1);
        }
        yield return new WaitForSeconds(0.1f);

        // Second Box
        puzzleObjects[1].SetActive(true);
        while (firstPersonController.raycastHitSomething == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        puzzleObjects[1].GetComponent<BoxCollider>().enabled = false;
        puzzleObjects[1].GetComponent<MeshRenderer>().material = enabledMaterial;
        playerCameraRoot.transform.LookAt(targets[1]);
        for (int i = 0; i < leaves1.Length; i++)
        {
            leaves1[i].GetComponent<MeshRenderer>().material.color = Color.Lerp(leaves1[i].GetComponent<MeshRenderer>().material.color, Color.gray, 1);
        }

        yield return new WaitForSeconds(0.1f);

        // Third && final Box
        puzzleObjects[2].SetActive(true);
        while (firstPersonController.raycastHitSomething == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        puzzleObjects[2].GetComponent<BoxCollider>().enabled = false;
        puzzleObjects[2].GetComponent<MeshRenderer>().material = enabledMaterial;

        // Disable boxes
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < puzzleObjects.Length; i++)
        {
            puzzleObjects[i].SetActive(false);
        }
        canvasMeshRenderer.material = canvasEndMaterial;

        yield return new WaitForSeconds(1f);

        // Puzzle finished, Player can move again
        Debug.Log("Player can rotate camera again");
        firstPersonController.canRotateCamera = true;
        Debug.Log("Player can move again");
        firstPersonController.canMove = true;

        // Activate tiles
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }

        // Open the doors
        for(int i = 0;i < doors.Length; i++)
        {
            doors[i].GetComponent<Animator>().enabled = true;  
        }

        Debug.Log("Player has completed the puzzle");
    }
}
