using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    public CheckerboardTileWaySpawner checkerboard2;
    private PlayerIsWithinTrigger playerIsWithinTrigger;
    private MyFirstPersonController firstPersonController;
    void Start()
    {
        playerIsWithinTrigger = GetComponent<PlayerIsWithinTrigger>();
        firstPersonController = GameObject.Find("PlayerCapsule").GetComponent<MyFirstPersonController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(firstPersonController.interacts)
            {
                GameObject.Find("Fase01_Dispositivo_Palanca").GetComponent<Animator>().SetTrigger("Play");
                Destroy(GameObject.Find("Fase01_PuertaZona1"));
                checkerboard2.spawn = true;
                Destroy(gameObject);
            }
        }
    }
}
