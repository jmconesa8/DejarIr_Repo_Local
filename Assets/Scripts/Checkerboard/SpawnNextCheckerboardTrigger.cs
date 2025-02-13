using UnityEngine;

public class SpawnNextCheckerboardTrigger : MonoBehaviour
{
    // Script spawns next checkerboard OnTriggerEnter
    public CheckerboardTileWaySpawner nextCheckerboard;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            nextCheckerboard.spawn = true;
        }
    }
}
