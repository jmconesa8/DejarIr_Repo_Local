using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform crossHairRectTransform;
    public Vector3 currentScale;
    private Vector3 one;
    private Vector3 two;
    void Start()
    {
        one = new Vector3(1, 1, 1);
        two = new Vector3(2, 2, 2);
        crossHairRectTransform = GetComponent<RectTransform>();
    }
    public void Update()
    {
        currentScale = gameObject.transform.localScale;
    }
    public void ChangeCrosshairSize()
    {
        
    }
}
