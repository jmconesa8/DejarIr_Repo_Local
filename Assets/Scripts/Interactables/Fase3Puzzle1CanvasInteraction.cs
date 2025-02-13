using UnityEngine;

public class Fase3Puzzle1CanvasInteraction : MonoBehaviour
{
    public bool puzzle1Complete;
    public bool puzzle2Complete;
    public bool puzzle3Complete;
    public bool puzzle4Complete;
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Tag1") == null)
        {
            puzzle1Complete = true;
        }
        if (GameObject.FindGameObjectWithTag("Tag2") == null)
        {
            puzzle2Complete = true;
        }
        if (GameObject.FindGameObjectWithTag("Tag3") == null)
        {
            puzzle3Complete = true;
        }
        if (GameObject.FindGameObjectWithTag("Tag4") == null)
        {
            puzzle4Complete = true;
        }
    }
}
