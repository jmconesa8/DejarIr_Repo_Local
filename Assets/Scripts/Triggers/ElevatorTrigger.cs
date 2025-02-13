using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorTrigger : MonoBehaviour
{
    private Animator elevatorAnimator;
    private GameObject playerCapsule;
    private GameObject playerCameraRoot;
    private MyFirstPersonController firstPersonController;
    void Start()
    {
        playerCapsule = GameObject.Find("PlayerCapsule");
        playerCameraRoot = GameObject.Find("PlayerCameraRoot");
        firstPersonController = playerCapsule.GetComponent<MyFirstPersonController>();
        elevatorAnimator = GameObject.Find("SM_elevator").GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(ElevatorSequence());
        }
    }

    public IEnumerator ElevatorSequence()
    {
        // Add code of elevator closing and player moving
        elevatorAnimator.SetBool("Close", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(4);
    }
}
