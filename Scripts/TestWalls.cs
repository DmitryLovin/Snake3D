using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalls : MonoBehaviour {

    private GameObject go;
    private Mesh cubeMesh, myMesh;
    public GameObject stone,light,_pl;

    private List<Vector3> WXN = new List<Vector3>();
    private List<Vector3> WXP = new List<Vector3>();
    private List<Vector3> WYN = new List<Vector3>();
    private List<Vector3> WYP = new List<Vector3>();
    private List<Vector3> WZN = new List<Vector3>();
    private List<Vector3> WZP = new List<Vector3>();

    List<Vector3> verts = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    List<int> triangs = new List<int>();
    List<Vector3> normals = new List<Vector3>();

    private MeshCollider meshCol;

    private void Start()
    {
        go = gameObject;
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();
        go.GetComponent<MeshRenderer>().material = stone.GetComponent<MeshRenderer>().sharedMaterial;
        Mesh tmp = stone.GetComponent<MeshFilter>().sharedMesh;
        cubeMesh = new Mesh();
        myMesh = new Mesh();
        Vector3[] vertexs = tmp.vertices;
        cubeMesh.vertices = vertexs;
        cubeMesh.uv = tmp.uv;
        cubeMesh.triangles = tmp.triangles;
        cubeMesh.normals = tmp.normals;
        meshCol = go.AddComponent<MeshCollider>();
    }

    public void SetWalls(int pattern) {

        List<Vector2> tmpVect = PatternLists.patterns[pattern];
        for (int i = 0; i < tmpVect.Count; i++)
        {
            WZN.Add(new Vector3(tmpVect[i].x, tmpVect[i].y, -10.5f));
            WZP.Add(new Vector3(tmpVect[i].x, tmpVect[i].y, 10.5f));
            WXN.Add(new Vector3(-10.5f, tmpVect[i].x, tmpVect[i].y));
            WXP.Add(new Vector3(10.5f, tmpVect[i].x, tmpVect[i].y));
            WYN.Add(new Vector3(tmpVect[i].x, -10.5f, tmpVect[i].y));
            WYP.Add(new Vector3(tmpVect[i].x, 10.5f, tmpVect[i].y));
        }
    }

    public void InitSpawn() {
        StartCoroutine("Create");
    }

    IEnumerator Create()
    {
        while (WXN.Count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            int rand = Random.Range(0, WXN.Count);
            Vector3 tmpVec = WXN[rand];
            if (Vector3.Distance(tmpVec, _pl.transform.position) > 5f)
            {
                int tmpl = verts.Count;
                for (int j = 0; j < cubeMesh.vertices.Length; j++)
                {
                    verts.Add(new Vector3(cubeMesh.vertices[j].x + tmpVec.x, cubeMesh.vertices[j].y + tmpVec.y, cubeMesh.vertices[j].z + tmpVec.z));
                    uvs.Add(cubeMesh.uv[j]);
                    normals.Add(cubeMesh.normals[j]);
                }
                for (int j = 0; j < cubeMesh.triangles.Length; j++)
                {
                    triangs.Add(cubeMesh.triangles[j] + tmpl);
                }
                WXN.RemoveAt(rand);
                myMesh.vertices = verts.ToArray();
                myMesh.uv = uvs.ToArray();
                myMesh.triangles = triangs.ToArray();
                myMesh.normals = normals.ToArray();
                go.GetComponent<MeshFilter>().sharedMesh = myMesh;
                meshCol.sharedMesh = myMesh;
                GameObject tmpLight = Instantiate(light, transform);
                tmpLight.transform.position = tmpVec;
                break;
            }
        }
        while (WXP.Count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            int rand = Random.Range(0, WXP.Count);
            Vector3 tmpVec = WXP[rand];
            if (Vector3.Distance(tmpVec, _pl.transform.position) > 5f)
            {
                int tmpl = verts.Count;
                for (int j = 0; j < cubeMesh.vertices.Length; j++)
                {
                    verts.Add(new Vector3(cubeMesh.vertices[j].x + tmpVec.x, cubeMesh.vertices[j].y + tmpVec.y, cubeMesh.vertices[j].z + tmpVec.z));
                    uvs.Add(cubeMesh.uv[j]);
                    normals.Add(cubeMesh.normals[j]);
                }
                for (int j = 0; j < cubeMesh.triangles.Length; j++)
                {
                    triangs.Add(cubeMesh.triangles[j] + tmpl);
                }
                WXP.RemoveAt(rand);
                myMesh.vertices = verts.ToArray();
                myMesh.uv = uvs.ToArray();
                myMesh.triangles = triangs.ToArray();
                myMesh.normals = normals.ToArray();
                go.GetComponent<MeshFilter>().sharedMesh = myMesh;
                meshCol.sharedMesh = myMesh;
                GameObject tmpLight = Instantiate(light, transform);
                tmpLight.transform.position = tmpVec;
                break;
            }
        }
        while (WYN.Count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            int rand = Random.Range(0, WYN.Count);
            Vector3 tmpVec = WYN[rand];
            if (Vector3.Distance(tmpVec, _pl.transform.position) > 5f)
            {
                int tmpl = verts.Count;
                for (int j = 0; j < cubeMesh.vertices.Length; j++)
                {
                    verts.Add(new Vector3(cubeMesh.vertices[j].x + tmpVec.x, cubeMesh.vertices[j].y + tmpVec.y, cubeMesh.vertices[j].z + tmpVec.z));
                    uvs.Add(cubeMesh.uv[j]);
                    normals.Add(cubeMesh.normals[j]);
                }
                for (int j = 0; j < cubeMesh.triangles.Length; j++)
                {
                    triangs.Add(cubeMesh.triangles[j] + tmpl);
                }
                WYN.RemoveAt(rand);
                myMesh.vertices = verts.ToArray();
                myMesh.uv = uvs.ToArray();
                myMesh.triangles = triangs.ToArray();
                myMesh.normals = normals.ToArray();
                go.GetComponent<MeshFilter>().sharedMesh = myMesh;
                meshCol.sharedMesh = myMesh;
                GameObject tmpLight = Instantiate(light, transform);
                tmpLight.transform.position = tmpVec;
                break;
            }
        }
        while (WYP.Count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            int rand = Random.Range(0, WYP.Count);
            Vector3 tmpVec = WYP[rand];
            if (Vector3.Distance(tmpVec, _pl.transform.position) > 5f)
            {
                int tmpl = verts.Count;
                for (int j = 0; j < cubeMesh.vertices.Length; j++)
                {
                    verts.Add(new Vector3(cubeMesh.vertices[j].x + tmpVec.x, cubeMesh.vertices[j].y + tmpVec.y, cubeMesh.vertices[j].z + tmpVec.z));
                    uvs.Add(cubeMesh.uv[j]);
                    normals.Add(cubeMesh.normals[j]);
                }
                for (int j = 0; j < cubeMesh.triangles.Length; j++)
                {
                    triangs.Add(cubeMesh.triangles[j] + tmpl);
                }
                WYP.RemoveAt(rand);
                myMesh.vertices = verts.ToArray();
                myMesh.uv = uvs.ToArray();
                myMesh.triangles = triangs.ToArray();
                myMesh.normals = normals.ToArray();
                go.GetComponent<MeshFilter>().sharedMesh = myMesh;
                meshCol.sharedMesh = myMesh;
                GameObject tmpLight = Instantiate(light, transform);
                tmpLight.transform.position = tmpVec;
                break;
            }
        }
        while (WZN.Count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            int rand = Random.Range(0, WZN.Count);
            Vector3 tmpVec = WZN[rand];
            if (Vector3.Distance(tmpVec, _pl.transform.position) > 5f)
            {
                int tmpl = verts.Count;
                for (int j = 0; j < cubeMesh.vertices.Length; j++)
                {
                    verts.Add(new Vector3(cubeMesh.vertices[j].x + tmpVec.x, cubeMesh.vertices[j].y + tmpVec.y, cubeMesh.vertices[j].z + tmpVec.z));
                    uvs.Add(cubeMesh.uv[j]);
                    normals.Add(cubeMesh.normals[j]);
                }
                for (int j = 0; j < cubeMesh.triangles.Length; j++)
                {
                    triangs.Add(cubeMesh.triangles[j] + tmpl);
                }
                WZN.RemoveAt(rand);
                myMesh.vertices = verts.ToArray();
                myMesh.uv = uvs.ToArray();
                myMesh.triangles = triangs.ToArray();
                myMesh.normals = normals.ToArray();
                go.GetComponent<MeshFilter>().sharedMesh = myMesh;
                meshCol.sharedMesh = myMesh;
                GameObject tmpLight = Instantiate(light, transform);
                tmpLight.transform.position = tmpVec;
                break;
            }
        }
        while (WZP.Count > 0)
        {
            yield return new WaitForSeconds(0.01f);
            int rand = Random.Range(0, WZP.Count);
            Vector3 tmpVec = WZP[rand];
            if (Vector3.Distance(tmpVec, _pl.transform.position) > 5f)
            {
                int tmpl = verts.Count;
                for (int j = 0; j < cubeMesh.vertices.Length; j++)
                {
                    verts.Add(new Vector3(cubeMesh.vertices[j].x + tmpVec.x, cubeMesh.vertices[j].y + tmpVec.y, cubeMesh.vertices[j].z + tmpVec.z));
                    uvs.Add(cubeMesh.uv[j]);
                    normals.Add(cubeMesh.normals[j]);
                }
                for (int j = 0; j < cubeMesh.triangles.Length; j++)
                {
                    triangs.Add(cubeMesh.triangles[j] + tmpl);
                }
                WZP.RemoveAt(rand);
                myMesh.vertices = verts.ToArray();
                myMesh.uv = uvs.ToArray();
                myMesh.triangles = triangs.ToArray();
                myMesh.normals = normals.ToArray();
                go.GetComponent<MeshFilter>().sharedMesh = myMesh;
                meshCol.sharedMesh = myMesh;
                GameObject tmpLight = Instantiate(light, transform);
                tmpLight.transform.position = tmpVec;
                break;
            }
        }
    }
}
