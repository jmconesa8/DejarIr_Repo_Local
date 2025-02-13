using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DropLooper : MonoBehaviour
{
    public bool playOnStart;

    [Header("SPAWN STRUCTURE")]
    [Range(1, 90)] public int spawnPointsNumber = 5;
    [Range(0.5f, 50)] public float spawnRadio;
    Vector3[] spawnPivots;
    [Header("SPAWN PARAMETERS")]
    [Range(0.15f, 5)] public float minShootTime;
    [Range(0.15f, 10)] public float maxShootTime;
    [Tooltip("Number of items that will be spawned per shoot")] public int spawnsPerShoot;

    public int maxItemsOnScene;
    public float maxTravelDistance;

    public Transform itemPoolContainer;
    public Transform specialPoolContainer;
    List<Transform> itemPool;
    List<Transform> spawnedItems;
    bool activated = true;
    bool isReady;
    [Header("DEBUG OPTIONS")]
    [Space(10)]
    public bool debug;
    void Start()
    {
    

        PoolSetUp();
        if (playOnStart)
        {
  

            StartCoroutine(SpawnItems());
        }
    }
    void PoolSetUp()
    {
        isReady = true;
        UpdateSpawnPoints();



        spawnedItems = new List<Transform>();


        itemPool = new List<Transform>();

        foreach (Transform g in itemPoolContainer.transform)
        {
            if (g == itemPoolContainer) continue;
            itemPool.Add(g);
        }



        int count = 0;
        for (int i = 0; i < maxItemsOnScene; i++)
        {
            try
            {

            if (count >= spawnPivots.Length)
            {
                count = 0;
            }

            GameObject newItemPool = GetPoolItem();
            if (newItemPool != null)
            {

                Vector3 newPos = transform.InverseTransformPoint(spawnPivots[count]);
                newPos.z = Random.Range(0, maxTravelDistance);
                newItemPool.transform.position = transform.TransformPoint(newPos);



                spawnedItems.Add(newItemPool.transform);
                itemPool.Remove(newItemPool.transform);

                ItemLocomotion(newItemPool);


            }





            count++;
            }
            catch
            {
                continue;
            }
        }


    }
    GameObject GetPoolItem()
    {

        int itemIndex = Random.Range(0, itemPool.Count);

        RandomDropItem item;
        if (itemPool[itemIndex].TryGetComponent<RandomDropItem>(out item))
        {
            float spawnProbability = item.spawnProbability;
            float randomNumber = Random.Range(0, 101);

            if (randomNumber <= spawnProbability && randomNumber != 0)
            {
                return itemPool[itemIndex].gameObject;
            }
            else
            {
                if(debug)
                {

              Debug.LogWarning("[DropLooper] GetPoolItem | item selected did not overcome the random umbral percent of: " + randomNumber);
                }

                return null;
            }

        }
        else
        {
            if(debug)
            {

            Debug.LogWarning("[DropLooper] GetPoolItem | item selected from pool hasn't got a 'RandomDrop' component attached and it's necessary: " + itemPool[itemIndex].name);
            }
            return null;

        }


    }
    IEnumerator DespawnItems()
    {
            if (spawnedItems==null)
            {
                spawnedItems = new List<Transform>();
            }
        while (activated)
        {

            List<Transform> itemsOut = new List<Transform>();
            if (spawnedItems.Count > 0)
            {

                foreach (Transform t in spawnedItems)
                {
                    if (transform.InverseTransformPoint(t.position).z > maxTravelDistance)
                    {

                        itemsOut.Add(t);
                    }
                }
                for (int i = 0; i < itemsOut.Count; i++)
                {
                    spawnedItems.Remove(itemsOut[i]);
                    itemsOut[i].position = itemPoolContainer.position;
                    itemsOut[i].GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

                    itemPool.Add(itemsOut[i]);

                    itemsOut[i].GetComponent<Collider>().gameObject.SetActive(false);
                }
            }
            else
            {
                if (debug )
                    {
                    Debug.LogWarning("[DropLooper] DespawnItems | there are not spawned items on scene to despawn");
                }

            }
            yield return null;
        }

    }
    IEnumerator SpawnItems()
    {

        StartCoroutine(DespawnItems());

        while (activated)
        {
            

            float shootTime = 0;
            if (minShootTime == maxShootTime)
            {
                shootTime = minShootTime;
            }

            else
            {
                shootTime = Random.Range(minShootTime, maxShootTime);
            }
            
            yield return new WaitForSeconds(shootTime);

            List<GameObject> itemsToShoot = new List<GameObject>();
            int spawnCount = 0;
            spawnsPerShoot = Mathf.Clamp(spawnsPerShoot, 0, spawnPivots.Length);
            while (spawnCount < spawnsPerShoot && spawnedItems.Count < maxItemsOnScene)
            {
                if (spawnedItems.Count < maxItemsOnScene && itemPool.Count > 0)
                {
                    //Random spawn pivot and random item


                    GameObject newDroppedItem = GetPoolItem();
                    while (newDroppedItem == null)
                    {
                        newDroppedItem = GetPoolItem();

                        yield return null;
                    }
                    itemsToShoot.Add(newDroppedItem);
                    spawnCount++;


                    spawnedItems.Add(newDroppedItem.transform);
                    itemPool.Remove(newDroppedItem.transform);
                }
                yield return null;
            }

            foreach (GameObject item in itemsToShoot)
            {

                int pivotIndex = Random.Range(0, spawnPivots.Length);

                item.transform.position = spawnPivots[pivotIndex];
                ItemLocomotion(item);
            }







            yield return null;
        }
    }
    void ItemLocomotion(GameObject item)
    {
        item.SetActive(true);



        RandomDropItem itemDrop = item.GetComponent<RandomDropItem>();

        #region IMPULSE  

        float spawnImpulse = itemDrop.spawnImpulse;

        float impulseVariance = (itemDrop.impulseVariancePercent * spawnImpulse) / 100;
        float randomVariance = Random.Range(0, impulseVariance);

        float randomImpulse = Random.Range(0, 2) > 0 ? spawnImpulse + randomVariance : spawnImpulse - randomVariance;

        item.GetComponent<Rigidbody>().AddForce(transform.forward * randomImpulse, ForceMode.Force);
        #endregion

        #region TORQUE
        if (itemDrop.randomTorque)
        {
            item.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere.normalized * itemDrop.torqueImpulse, ForceMode.Force);

        }
        else
        {

            item.GetComponent<Rigidbody>().AddTorque(itemDrop.initialTorque.normalized * itemDrop.torqueImpulse, ForceMode.Force);
        }

        #endregion


    }
    void Update()
    {
        UpdateSpawnPoints();

    }
    void UpdateSpawnPoints()
    {
        spawnPivots = new Vector3[spawnPointsNumber];

        Vector3 newPos = transform.position + (transform.up * spawnRadio);

        for (int i = 0; i < spawnPointsNumber; i++)
        {
            spawnPivots[i] = newPos;

            newPos = (Quaternion.AngleAxis((360 / spawnPointsNumber) * (i + 1), transform.forward) * transform.up * spawnRadio) + transform.position;
        }
    }
#if UNITY_EDITOR
    public void OnDrawGizmos()
    {

        if (!EditorApplication.isPlaying) { UpdateSpawnPoints(); }
        foreach (Vector3 p in spawnPivots)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(p, Gizmos.probeSize);
        }
        Debug.DrawRay(transform.position, transform.forward * maxTravelDistance, Color.yellow);
    }
#endif

    public void ActivateDropper()

    {
        if(!isReady)
        {
            PoolSetUp();
        }
        StartCoroutine(SpawnItems());
    }
    public void StopItemsOnScene()
    {
        foreach(Transform t in spawnedItems)
        {
            t.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            t.GetComponent<Rigidbody>().isKinematic=false;
        }
    }
}
