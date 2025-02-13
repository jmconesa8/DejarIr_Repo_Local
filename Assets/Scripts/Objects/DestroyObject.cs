using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void OnDestroy()
    {
        Debug.Log(gameObject.name + " will be destroyed");
        Destroy(gameObject);
    }
}
