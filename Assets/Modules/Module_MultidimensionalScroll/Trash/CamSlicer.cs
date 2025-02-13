using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CamSlicer : MonoBehaviour
{
  

    float timer = 1;

    struct Triangle
    {
        public Vector3 v1;
        public Vector3 v2;
        public Vector3 v3;

        public Vector3 getNormal()
        {
            return Vector3.Cross(v1 - v2, v1 - v3).normalized;
        }

        // Conver direction to point in the direction of the tri
        public void matchDirection(Vector3 dir)
        {
            if (Vector3.Dot(getNormal(), dir) > 0)
            {
                return;
            }
            else
            {
                Vector3 vec = v1;
                v1 = v3;
                v3 = vec;
            }
        }
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(MakePhoto());
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("[CamSlicer] - Item detected");

        if (timer <= 0)
        {
            if (other.gameObject.tag == "Slicable")
            {
                timer += 1;
                Debug.Log("[CamSlicer] - Slice collided slicable item");

            }
        }
    }
    List<Transform> itemsSliced ;

    IEnumerator  MakePhoto()
    {
        Debug.Log("[CamSlicer] - To take a photo");
        itemsSliced = new List<Transform>();

        Vector3[] frustumCorners = new Vector3[4];
        Camera camera = GetComponent<Camera>();
        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);


        for (int i = 0; i < 4; i++)
        {
            var worldSpaceCorner = camera.transform.TransformPoint(frustumCorners[i]);
            Debug.DrawRay(camera.transform.position, (worldSpaceCorner-transform.position).normalized* (worldSpaceCorner - transform.position).magnitude, Color.blue, Mathf.Infinity);

            //GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube2.transform.position =worldSpaceCorner;
        }
        Vector3[] planePoints = new Vector3[3]
        {
            camera.transform.position,
            camera.transform.TransformPoint(frustumCorners[0]),
            camera.transform.TransformPoint(frustumCorners[1])
        };

        DetectObjectsByPlane(planePoints);
        yield return new WaitForSeconds(0.15f);
        itemsSliced = new List<Transform>();

        planePoints = new Vector3[3]
        {
            camera.transform.position,
            camera.transform.TransformPoint(frustumCorners[1]),
            camera.transform.TransformPoint(frustumCorners[2])
        };
        itemsSliced = new List<Transform>();

        DetectObjectsByPlane(planePoints);

        yield return new WaitForSeconds(0.15f);

        planePoints = new Vector3[3]
             {
            camera.transform.position,
            camera.transform.TransformPoint(frustumCorners[2]),
            camera.transform.TransformPoint(frustumCorners[3])
             };
        itemsSliced = new List<Transform>();

        DetectObjectsByPlane(planePoints);

        itemsSliced = new List<Transform>();
        yield return new WaitForSeconds(0.15f);
        planePoints = new Vector3[3]
    {
            camera.transform.position,
            camera.transform.TransformPoint(frustumCorners[3]),
            camera.transform.TransformPoint(frustumCorners[0])
    };
        yield return new WaitForSeconds(0.15f);

        itemsSliced = new List<Transform>();

        DetectObjectsByPlane(planePoints);


        List<Transform> itemsDetected= new List<Transform>();
        
        Collider[] colliders = FindObjectsOfType(typeof(Collider)) as Collider[];
        foreach (Collider coll in colliders)
        {
            Vector2 objectViewPortPivot = GetComponent<Camera>().WorldToViewportPoint(coll.bounds.center);
            if (objectViewPortPivot.x>0
                && objectViewPortPivot.x<1
                && objectViewPortPivot.y>0
                && objectViewPortPivot.y<1)
            {
                itemsDetected.Add(coll.gameObject.transform);
            }
        }

        for(int i=0; i< itemsDetected.Count;i++)
        {
          Destroy(itemsDetected[i].gameObject);
        }

    }
    void DetectObjectsByPlane(Vector3[] planePoints, UnityAction action=null)
    {

        //GameObject cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube2.transform.position = planePoints[1];
        //GameObject cube3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube3.transform.position = planePoints[2];

        int subdivisionFactor = 10;
        float distanceBetweenPoints = Mathf.Abs((planePoints[2] - planePoints[1]).magnitude)/subdivisionFactor;
        Vector3 farPointsDir = (planePoints[2] - planePoints[1]).normalized;
        Vector3 currentPoint = planePoints[1]+farPointsDir* distanceBetweenPoints;
        for(int i=0;i <subdivisionFactor-1;i++)
        {
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = currentPoint;
            Debug.DrawRay(transform.position, (currentPoint - transform.position).normalized * (currentPoint - transform.position).magnitude, Color.green, Mathf.Infinity);
            RaycastHit[] hits = Physics.RaycastAll(transform.position, (currentPoint-transform.position).normalized, (currentPoint - transform.position).magnitude, LayerMask.NameToLayer("Slicable"));

            foreach (RaycastHit hit in hits)
            {
                if(!itemsSliced.Contains(hit.transform))
                {
                    itemsSliced.Add(hit.transform);
                slice(planePoints, hit); 
                }
            }   




            currentPoint = currentPoint + farPointsDir * distanceBetweenPoints;
        }

    }

    void slice(Vector3[] planePoints, RaycastHit other)
    {


        Collider coll = GetComponent<Collider>();

        #region Create the cutting plane
        //Vector3 vec1 = coll.bounds.center;
        //vec1 += transform.up * coll.bounds.extents.y;
        Vector3 vec1 = planePoints[1];




        //Vector3 vec2 = coll.bounds.center;
        //vec2 += transform.up * coll.bounds.extents.y;
        //vec2 += transform.right * coll.bounds.extents.x;
        Vector3 vec2 = planePoints[0];


        //Vector3 vec3 = coll.bounds.center;
        //vec3 += transform.up * -coll.bounds.extents.y;
        //vec3 += transform.right * coll.bounds.extents.x;
        Vector3 vec3 = planePoints[2];


        Plane pl = new Plane(vec1, vec2, vec3);
        #endregion

        Transform tr = other.transform;
        Mesh m = other.transform.gameObject.GetComponent<MeshFilter>().mesh;
        int[] triangles = m.triangles;
        Vector3[] verts = m.vertices;

        List<Vector3> intersections = new List<Vector3>();
        List<Triangle> newTris1 = new List<Triangle>();
        List<Triangle> newTris2 = new List<Triangle>();

        // Loop through tris
        for (int i = 0; i < triangles.Length; i += 3)
        {
            List<Vector3> points = new List<Vector3>();

            int v1 = triangles[i];
            int v2 = triangles[i + 1];
            int v3 = triangles[i + 2];
            Vector3 p1 = tr.TransformPoint(verts[v1]);
            Vector3 p2 = tr.TransformPoint(verts[v2]);
            Vector3 p3 = tr.TransformPoint(verts[v3]);
            Vector3 norm = Vector3.Cross(p1 - p2, p1 - p3);

            Vector3 dir = p2 - p1;
            float ent;

            // Check if tris are intersected
            if (pl.Raycast(new Ray(p1, dir), out ent) && ent <= dir.magnitude)
            {
                Vector3 intersection = p1 + ent * dir.normalized;
                intersections.Add(intersection);
                points.Add(intersection);
            }
            dir = p3 - p2;
            if (pl.Raycast(new Ray(p2, dir), out ent) && ent <= dir.magnitude)
            {
                Vector3 intersection = p2 + ent * dir.normalized;
                intersections.Add(intersection);
                points.Add(intersection);
            }
            dir = p3 - p1;
            if (pl.Raycast(new Ray(p1, dir), out ent) && ent <= dir.magnitude)
            {
                Vector3 intersection = p1 + ent * dir.normalized;
                intersections.Add(intersection);
                points.Add(intersection);
            }

            // Group tris and create new tris
            if (points.Count > 0)
            {
                Debug.Assert(points.Count == 2);
                List<Vector3> points1 = new List<Vector3>();
                List<Vector3> points2 = new List<Vector3>();
                // Intersection verts
                points1.AddRange(points);
                points2.AddRange(points);
                // Check which side the original vert was
                if (pl.GetSide(p1))
                {
                    points1.Add(p1);
                }
                else
                {
                    points2.Add(p1);
                }
                if (pl.GetSide(p2))
                {
                    points1.Add(p2);
                }
                else
                {
                    points2.Add(p2);
                }
                if (pl.GetSide(p3))
                {
                    points1.Add(p3);
                }
                else
                {
                    points2.Add(p3);
                }

                if (points1.Count == 3)
                {
                    Triangle tri = new Triangle() { v1 = points1[1], v2 = points1[0], v3 = points1[2] };
                    tri.matchDirection(norm);
                    newTris1.Add(tri);
                }
                else
                {
                    Debug.Assert(points1.Count == 4);
                    if (Vector3.Dot((points1[0] - points1[1]), points1[2] - points1[3]) >= 0)
                    {
                        Triangle tri = new Triangle() { v1 = points1[0], v2 = points1[2], v3 = points1[3] };
                        tri.matchDirection(norm);
                        newTris1.Add(tri);
                        tri = new Triangle() { v1 = points1[0], v2 = points1[3], v3 = points1[1] };
                        tri.matchDirection(norm);
                        newTris1.Add(tri);
                    }
                    else
                    {
                        Triangle tri = new Triangle() { v1 = points1[0], v2 = points1[3], v3 = points1[2] };
                        tri.matchDirection(norm);
                        newTris1.Add(tri);
                        tri = new Triangle() { v1 = points1[0], v2 = points1[2], v3 = points1[1] };
                        tri.matchDirection(norm);
                        newTris1.Add(tri);
                    }
                }

                if (points2.Count == 3)
                {
                    Triangle tri = new Triangle() { v1 = points2[1], v2 = points2[0], v3 = points2[2] };
                    tri.matchDirection(norm);
                    newTris2.Add(tri);
                }
                else
                {
                    Debug.Assert(points2.Count == 4);
                    if (Vector3.Dot((points2[0] - points2[1]), points2[2] - points2[3]) >= 0)
                    {
                        Triangle tri = new Triangle() { v1 = points2[0], v2 = points2[2], v3 = points2[3] };
                        tri.matchDirection(norm);
                        newTris2.Add(tri);
                        tri = new Triangle() { v1 = points2[0], v2 = points2[3], v3 = points2[1] };
                        tri.matchDirection(norm);
                        newTris2.Add(tri);
                    }
                    else
                    {
                        Triangle tri = new Triangle() { v1 = points2[0], v2 = points2[3], v3 = points2[2] };
                        tri.matchDirection(norm);
                        newTris2.Add(tri);
                        tri = new Triangle() { v1 = points2[0], v2 = points2[2], v3 = points2[1] };
                        tri.matchDirection(norm);
                        newTris2.Add(tri);
                    }
                }
            }
            else
            {
                if (pl.GetSide(p1))
                {
                    newTris1.Add(new Triangle() { v1 = p1, v2 = p2, v3 = p3 });
                }
                else
                {
                    newTris2.Add(new Triangle() { v1 = p1, v2 = p2, v3 = p3 });
                }
            }
        }

        if (intersections.Count > 1)
        {
            // Sets center
            Vector3 center = Vector3.zero;
            foreach (Vector3 vec in intersections)
            {
                center += vec;
            }
            center /= intersections.Count;
            for (int i = 0; i < intersections.Count; i++)
            {
                Triangle tri = new Triangle() { v1 = intersections[i], v2 = center, v3 = i + 1 == intersections.Count ? intersections[i] : intersections[i + 1] };
                tri.matchDirection(-pl.normal);
                newTris1.Add(tri);
            }
            for (int i = 0; i < intersections.Count; i++)
            {
                Triangle tri = new Triangle() { v1 = intersections[i], v2 = center, v3 = i + 1 == intersections.Count ? intersections[i] : intersections[i + 1] };
                tri.matchDirection(pl.normal);
                newTris2.Add(tri);
            }
        }

        if (intersections.Count > 0)
        {
            // Creates new meshes
            Material mat = other.transform.gameObject.GetComponent<MeshRenderer>().material;
            Destroy(other.transform.gameObject);

            Mesh mesh1 = new Mesh();
            Mesh mesh2 = new Mesh();

            List<Vector3> tris = new List<Vector3>();
            List<int> indices = new List<int>();

            int index = 0;
            foreach (Triangle thing in newTris1)
            {
                tris.Add(thing.v1);
                tris.Add(thing.v2);
                tris.Add(thing.v3);
                indices.Add(index++);
                indices.Add(index++);
                indices.Add(index++);
            }
            mesh1.vertices = tris.ToArray();
            mesh1.triangles = indices.ToArray();

            index = 0;
            tris.Clear();
            indices.Clear();
            foreach (Triangle thing in newTris2)
            {
                tris.Add(thing.v1);
                tris.Add(thing.v2);
                tris.Add(thing.v3);
                indices.Add(index++);
                indices.Add(index++);
                indices.Add(index++);
            }
            mesh2.vertices = tris.ToArray();
            mesh2.triangles = indices.ToArray();

            mesh1.RecalculateNormals();

            mesh1.RecalculateBounds();
            mesh2.RecalculateNormals();
            mesh2.RecalculateBounds();

            // Create new objects

            GameObject go1 = new GameObject();
            GameObject go2 = new GameObject();

            MeshFilter mf1 = go1.AddComponent<MeshFilter>();
            mf1.mesh = mesh1;
            MeshRenderer mr1 = go1.AddComponent<MeshRenderer>();
            mr1.material = mat;
            MeshCollider mc1 = go1.AddComponent<MeshCollider>();
            //if (mf1.mesh.vertexCount <= 255) {
            mc1.convex = true;
            go1.AddComponent<Rigidbody>();

            //}
            mc1.sharedMesh = mesh1;
            go1.tag = "Slicable";

      
            MeshFilter mf2 = go2.AddComponent<MeshFilter>();
            mf2.mesh = mesh2;
            MeshRenderer mr2 = go2.AddComponent<MeshRenderer>();
            mr2.material = mat;
            MeshCollider mc2 = go2.AddComponent<MeshCollider>();
            //if (mf2.mesh.vertexCount <= 255) {
            mc2.convex = true;
            go2.AddComponent<Rigidbody>();

            //}
            mc2.sharedMesh = mesh2;
            go2.tag = "Slicable";

            Debug.LogWarning(GetComponent<Camera>().WorldToViewportPoint(go1.transform.position).x);
            Debug.LogWarning(GetComponent<Camera>().WorldToViewportPoint(go2.transform.position).x);

            itemsSliced.Add(go1.transform);

            Vector2 objectViewPortPivot = GetComponent<Camera>().WorldToViewportPoint(go1.GetComponent<Collider>().bounds.center);
            if (objectViewPortPivot.x > 0
                && objectViewPortPivot.x < 1
                && objectViewPortPivot.y > 0
                && objectViewPortPivot.y < 1)
            {
                go1.GetComponent<Rigidbody>().useGravity = false;
                go1.GetComponent<Rigidbody>().isKinematic = true;
            }


            itemsSliced.Add(go2.transform);


             objectViewPortPivot = GetComponent<Camera>().WorldToViewportPoint(go2.GetComponent<Collider>().bounds.center);
            if (objectViewPortPivot.x > 0
                && objectViewPortPivot.x < 1
                && objectViewPortPivot.y > 0
                && objectViewPortPivot.y < 1)
            {
                go2.GetComponent<Rigidbody>().useGravity = false;
                go2.GetComponent<Rigidbody>().isKinematic = true;
            }
           
        }

    }
}

