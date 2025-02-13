using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script para la creación específica de un efecto cubo no euclidiano con múltiples "dimensiones"
/// De momento es necesario que en el set up inicial el forward del cubo mire hacia el jugador para que funcione correctamente el scroll
/// </summary>
public class MultidimensionalScroll : MonoBehaviour
{
    public Transform player;
    public Transform scrollPivot;
    public GameObject[] itemPool;
    public GameObject[] cubeItemPivots;
            [Space(2)]
            [Space(2)]
            [Header("____________________")]
    public GameObject[] currentCubeSidesItems;




    int indexItems = 0;
    int indexFace=0;


    void Awake()
    {

        currentCubeSidesItems[indexFace]= Instantiate(itemPool[indexFace], cubeItemPivots[indexFace].transform) as GameObject;
        currentCubeSidesItems[indexFace].transform.localPosition = Vector3.zero;
        currentCubeSidesItems[indexFace].transform.localRotation = Quaternion.identity;
        currentCubeSidesItems[indexFace].transform.localScale = new Vector3(1, 1, 1);
        foreach(Transform t in currentCubeSidesItems[indexFace].transform)
        {
            Renderer[] meshes = t.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in meshes)
            {

                r.material.SetFloat("_StencilMask", 1);
            }

        }


        currentCubeSidesItems[GetAdjacentFaceIndex(true)] = Instantiate(itemPool[GetAdjacentMeshIndex(true)], cubeItemPivots[GetAdjacentFaceIndex(true)].transform) as GameObject;
        currentCubeSidesItems[GetAdjacentFaceIndex(true)].transform.localPosition = Vector3.zero;
        currentCubeSidesItems[GetAdjacentFaceIndex(true)].transform.localRotation = Quaternion.identity;
        currentCubeSidesItems[GetAdjacentFaceIndex(true)].transform.localScale = new Vector3(1, 1, 1);
        foreach (Transform t in currentCubeSidesItems[GetAdjacentFaceIndex(true)].transform)
        {

            Renderer[] meshes = t.GetComponentsInChildren<Renderer>();

         foreach(Renderer r in meshes)
            {

                r.material.SetFloat("_StencilMask", GetAdjacentFaceIndex(true) + 1);
            }
                

        }



        currentCubeSidesItems[GetAdjacentFaceIndex(false)] = Instantiate(itemPool[GetAdjacentMeshIndex(false)], cubeItemPivots[GetAdjacentFaceIndex(false)].transform) as GameObject;
        currentCubeSidesItems[GetAdjacentFaceIndex(false)].transform.localPosition = Vector3.zero;
        currentCubeSidesItems[GetAdjacentFaceIndex(false)].transform.localRotation = Quaternion.identity;
        currentCubeSidesItems[GetAdjacentFaceIndex(false)].transform.localScale = new Vector3(1, 1, 1);
        foreach (Transform t in currentCubeSidesItems[GetAdjacentFaceIndex(false)].transform)
        {
            Renderer[] meshes = t.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in meshes)
            {

                r.material.SetFloat("_StencilMask", GetAdjacentFaceIndex(false) + 1);
            }
        }




        //cubeSidesItems[indexFace] = Instantiate( itemPool[indexItems]) as GameObject;
        //cubeSidesItems[indexFace+1] = Instantiate(itemPool[indexItems+1]) as GameObject;

        //cubeSidesItems[indexFace+2] = Instantiate(itemPool[indexItems+2]) as GameObject; 

        //cubeSidesItems[indexFace+3] = Instantiate(itemPool[indexItems+3]) as GameObject; 

        if (currentCubeSidesItems.Length>4)
        {
        Debug.LogError("[MultidimensionalScroll] - Start | 'cubeSidesItems' contains more than 4 elements. This array must to have just 4 elements");
            
        }
        DebugIndex();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pointA =  transform.forward;

        Vector3 pointB = (new Vector3(player.position.x,0, player.position.z) -new Vector3(scrollPivot.transform.position.x, 0,scrollPivot.transform.position.z)).normalized;
        float angleBetweenPlayer = Vector3.SignedAngle(   pointB, pointA, Vector3.up);


        if(angleBetweenPlayer>=90)
        {
            RotatePivot(false);
        }
        else if(angleBetweenPlayer<=-90)
        {
            RotatePivot(true);

        }





        Debug.LogWarning(angleBetweenPlayer);
      // Debug.LogWarning("pseudo "+(angleBetweenPlayer*-1));

    }
    void RotatePivot(bool positive)
    {
        int angle = positive ? 1 : -1;
        angle *= 90;

        Quaternion newRot =scrollPivot.transform.rotation * Quaternion.AngleAxis(angle, Vector3.up);

       scrollPivot.transform.rotation = newRot;
        UpdateFaceIndex(positive);
    }
    void UpdateFaceIndex(bool positive)
    {

        int prevIndex = indexFace;
        indexFace = positive ? indexFace - 1 : indexFace + 1;
        indexFace = indexFace < 0 ? 3 : indexFace;
        indexFace = indexFace > 3 ? 0 : indexFace;

        indexItems = positive ? indexItems - 1 : indexItems + 1;
        indexItems = indexItems < 0 ? itemPool.Length-1 : indexItems;
        indexItems = indexItems > itemPool.Length - 1 ? 0 : indexItems;


        DebugIndex();
        //cubeSidesItems[indexFace].mesh = itemPool[indexItems];

        GameObject old1 = currentCubeSidesItems[GetAdjacentFaceIndex(true)];
        GameObject old2 = currentCubeSidesItems[GetAdjacentFaceIndex(false)];


        Destroy(old1);
        Destroy(old2);




        currentCubeSidesItems[GetAdjacentFaceIndex(true)] = Instantiate(itemPool[GetAdjacentMeshIndex(true)], cubeItemPivots[GetAdjacentFaceIndex(true)].transform) as GameObject;
        currentCubeSidesItems[GetAdjacentFaceIndex(true)].transform.localPosition = Vector3.zero;
        currentCubeSidesItems[GetAdjacentFaceIndex(true)].transform.localRotation = Quaternion.identity;
        currentCubeSidesItems[GetAdjacentFaceIndex(true)].transform.localScale = new Vector3(1, 1, 1);
        foreach (Transform t in currentCubeSidesItems[GetAdjacentFaceIndex(true)].transform)
        {
            Renderer[] meshes = t.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in meshes)
            {

                r.material.SetFloat("_StencilMask", GetAdjacentFaceIndex(true) + 1);
            }


        }



        currentCubeSidesItems[GetAdjacentFaceIndex(false)] = Instantiate(itemPool[GetAdjacentMeshIndex(false)], cubeItemPivots[GetAdjacentFaceIndex(false)].transform) as GameObject;
        currentCubeSidesItems[GetAdjacentFaceIndex(false)].transform.localPosition = Vector3.zero;
        currentCubeSidesItems[GetAdjacentFaceIndex(false)].transform.localRotation = Quaternion.identity;
        currentCubeSidesItems[GetAdjacentFaceIndex(false)].transform.localScale = new Vector3(1, 1, 1);
        foreach (Transform t in currentCubeSidesItems[GetAdjacentFaceIndex(false)].transform)
        {
            Renderer[] meshes = t.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in meshes)
            {

                r.material.SetFloat("_StencilMask", GetAdjacentFaceIndex(false) + 1);
            }
        }







        //itemCanvas1.mesh = itemPool[indexItems];
        //    itemCanvas2.mesh = itemPool[indexItems + 1];
        //    itemCanvas3.mesh = itemPool[indexItems + 2];
        //    itemCanvas4.mesh = itemPool[indexItems + 3];

    }


    void DebugIndex()
    {
        Debug.Log("[MultidimensionalScroll] - UpdateFaceIndex | current indexFace: " + indexFace);
        Debug.Log("[MultidimensionalScroll] - UpdateFaceIndex | prev indexFace: " + GetAdjacentFaceIndex(false));
        Debug.Log("[MultidimensionalScroll] - UpdateFaceIndex | next indexFace: " + GetAdjacentFaceIndex(true));
        Debug.Log("[MultidimensionalScroll] - UpdateFaceIndex | _________________________________________");
        Debug.Log("[MultidimensionalScroll] - UpdateFaceIndex | current indexMesh: " + indexItems);
        Debug.Log("[MultidimensionalScroll] - UpdateFaceIndex | prev indexItems: " + GetAdjacentMeshIndex(false));
        Debug.Log("[MultidimensionalScroll] - UpdateFaceIndex | next indexItems: " + GetAdjacentMeshIndex(true));

    }
    int GetAdjacentFaceIndex(bool positive)
    {
        int newIndex = indexFace;
        newIndex = positive ? newIndex+1 : newIndex-1;


        newIndex = newIndex < 0 ? 3 : newIndex;
        newIndex = newIndex > 3 ? 0 : newIndex;
        Debug.Log("[MultidimensionalScroll] - GetAdjacentIndex | returning: "+newIndex);

        return newIndex;

    }
    int GetAdjacentMeshIndex(bool positive)
    {
        int newIndex = indexItems;
        newIndex = positive ? newIndex + 1 : newIndex - 1;


        newIndex = newIndex < 0 ? itemPool.Length-1 : newIndex;
        newIndex = newIndex > itemPool.Length - 1 ? 0 : newIndex;
        Debug.Log("[MultidimensionalScroll] - GetAdjacentMeshIndex | returning: " + newIndex);

        return newIndex;

    }

    void UpdateCubeMeshes()
    {


    }
}
