using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTrigger : MonoBehaviour
{
    public Animator animator;
    private PlayerIsWithinTrigger playerIsWithinTrigger;

    void Start()
    {
        //animator = GetComponent<Animator>();
        //playerIsWithinTrigger = GetComponent<PlayerIsWithinTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(ScriptedSequence());
        }
    }

    public IEnumerator ScriptedSequence()
    {
        animator.SetBool("Play", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(3);
        yield return null;
    }
}
