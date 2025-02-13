using System.Collections;
using UnityEngine;

public class Fase1Puzzle2CanvasInteraction : MonoBehaviour
{
    // Script for Puzzle 2 in Fase 1
    [Tooltip("Canvas mesh renderer")]
    public MeshRenderer canvasMeshRenderer;
    [Tooltip("The material that goes on the canvas at the end of the puzzle")]
    public Material canvasEndMaterial;
    [Space(10)]
    [Tooltip("Objects on the canvas MUST BE RAYCAST LAYER")]
    public GameObject[] puzzleObjects;
    [Header("Materials for the objects once enabled")]
    public Material enabledMaterial;
    [Space(10)]
    public GameObject cuadro2Trigger;
    public CheckerboardTileWaySpawner checkerboard1;

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

        // Puzzle 2 exclusive
        if(cuadro2Trigger.activeSelf == true)
        {
            cuadro2Trigger.SetActive(false);
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

        Debug.Log("Begin Tutorial puzzle");
        // Player can't move until the end of the puzzle
        firstPersonController.canMove = false;
        yield return new WaitForSeconds(0.5f);
        // Select puzzle objects
        for (int i = 0; i < puzzleObjects.Length; i++)
        {
            puzzleObjects[i].SetActive(true);
            // Wait until Player interacts with it
            while (firstPersonController.raycastHitSomething == false)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            puzzleObjects[i].GetComponent<SphereCollider>().enabled = false;
            puzzleObjects[i].GetComponent<MeshRenderer>().material = enabledMaterial;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);
        // Deactivate puzzle objects
        for (int i = 0; i < puzzleObjects.Length; i++)
        {
            puzzleObjects[(int)i].SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        canvasMeshRenderer.material = canvasEndMaterial;
        cuadro2Trigger.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        checkerboard1.spawn = true;
        firstPersonController.canMove = true;
        Debug.Log("Player has completed puzzle 1");
    }
}
