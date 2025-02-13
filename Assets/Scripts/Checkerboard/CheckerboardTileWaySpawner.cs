using System.Collections;
using UnityEngine;

public class CheckerboardTileWaySpawner : MonoBehaviour
{
    // Script goes on the checkerboard tiles
    [Tooltip("Tiles spawn when true")]
    public bool spawn;
    public GameObject[] tiles;
    [Header("Don't  fill array if autofills")]
    public bool autofills;
    private bool crBool;
    private void Start()
    {
        if (autofills)
        {
            // Get the GameObject you want to access the children of
            GameObject parentObject = gameObject; // Or assign a specific GameObject

            // Initialize the array with the correct size
            tiles = new GameObject[parentObject.transform.childCount];

            // Disables the tiles
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {
                tiles[i] = parentObject.transform.GetChild(i).gameObject;
                tiles[i].SetActive(false);
            }
        }
    }
    void Update()
    {
        if(spawn && !crBool)
        {
            StartCoroutine(SpawnTiles());
        }
    }

    public IEnumerator SpawnTiles()
    {
        crBool = true;
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
