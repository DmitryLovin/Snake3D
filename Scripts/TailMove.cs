using System.Collections.Generic;
using UnityEngine;

public class TailMove : MonoBehaviour
{

    Mesh mesh;

    public float distance;
    public GameObject _pl;
    public float size;
    public float offsetDown;

    private bool delCent = false;

    private List<Vector3> centers = new List<Vector3>();
    private List<Vector3> vertices = new List<Vector3>();
    private List<Vector3> normals = new List<Vector3>();
    public List<Vector2> uvs = new List<Vector2>();
    private List<int> triangles = new List<int>();

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        GetComponent<SkinnedMeshRenderer>().sharedMesh = mesh;
        vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position + _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);

        vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position + _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);

        vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position + _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);

        centers.Add(_pl.transform.position + _pl.transform.up * -offsetDown);
        centers.Add(_pl.transform.position + _pl.transform.up * -offsetDown);

        normals.Add(-_pl.transform.forward);
        normals.Add(-_pl.transform.forward);
        normals.Add(-_pl.transform.forward);
        normals.Add(-_pl.transform.forward);

        normals.Add(_pl.transform.up);
        normals.Add(_pl.transform.up);
        normals.Add(_pl.transform.right);
        normals.Add(_pl.transform.right);

        normals.Add(-_pl.transform.right);
        normals.Add(-_pl.transform.right);
        normals.Add(_pl.transform.up);
        normals.Add(_pl.transform.up);

        normals.Add(_pl.transform.right);
        normals.Add(_pl.transform.right);
        normals.Add(-_pl.transform.right);
        normals.Add(-_pl.transform.right);

        uvs.Add(new Vector2(1, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(1, 0));
        uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(1, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 1));

        triangles.Add(1);
        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(3);
        triangles.Add(0);
        triangles.Add(2);

        triangles.Add(4);
        triangles.Add(5);
        triangles.Add(10);
        triangles.Add(10);
        triangles.Add(5);
        triangles.Add(11);

        triangles.Add(7);
        triangles.Add(6);
        triangles.Add(13);
        triangles.Add(13);
        triangles.Add(6);
        triangles.Add(12);

        triangles.Add(8);
        triangles.Add(9);
        triangles.Add(14);
        triangles.Add(14);
        triangles.Add(9);
        triangles.Add(15);

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    void LateUpdate()
    {
        vertices[vertices.Count - 1] = _pl.transform.position - _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown;
        vertices[vertices.Count - 2] = _pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
        vertices[vertices.Count - 3] = _pl.transform.position + _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown;
        vertices[vertices.Count - 4] = _pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
        vertices[vertices.Count - 5] = _pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
        vertices[vertices.Count - 6] = _pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
        centers[centers.Count - 1] = _pl.transform.position + _pl.transform.up * -offsetDown;

        float speed = _pl.GetComponent<SnakeMove>().speed;

        float curDist = getDistance();

        if (curDist > distance)
        {
            if (curDist - distance > Time.deltaTime * speed)
            {
                speed *= (curDist / distance);
            }

            if (Vector3.Distance(vertices[0], centers[1]) > Vector3.Distance(vertices[1], centers[1]))
            {
                float difDist = (Vector3.Distance(vertices[0], centers[1]) - Vector3.Distance(vertices[1], centers[1]))/5f;
                vertices[0] = Vector3.MoveTowards(vertices[0], vertices[10], Time.deltaTime * speed + difDist);
                vertices[1] = Vector3.MoveTowards(vertices[1], vertices[11], Time.deltaTime * speed);
                vertices[2] = Vector3.MoveTowards(vertices[2], vertices[13], Time.deltaTime * speed + difDist);
                vertices[3] = Vector3.MoveTowards(vertices[3], vertices[15], Time.deltaTime * speed);
                vertices[4] = Vector3.MoveTowards(vertices[4], vertices[10], Time.deltaTime * speed + difDist);
                vertices[5] = Vector3.MoveTowards(vertices[5], vertices[11], Time.deltaTime * speed);
                vertices[6] = Vector3.MoveTowards(vertices[6], vertices[12], Time.deltaTime * speed + difDist);
                vertices[7] = Vector3.MoveTowards(vertices[7], vertices[13], Time.deltaTime * speed + difDist);
                vertices[8] = Vector3.MoveTowards(vertices[8], vertices[14], Time.deltaTime * speed);
                vertices[9] = Vector3.MoveTowards(vertices[9], vertices[15], Time.deltaTime * speed);
            }
            else if (Vector3.Distance(vertices[0], centers[1]) < Vector3.Distance(vertices[1], centers[1])) {
                float difDist = (Vector3.Distance(vertices[1], centers[1]) - Vector3.Distance(vertices[0], centers[1]))/5f;
                vertices[0] = Vector3.MoveTowards(vertices[0], vertices[10], Time.deltaTime * speed);
                vertices[1] = Vector3.MoveTowards(vertices[1], vertices[11], Time.deltaTime * speed + difDist);
                vertices[2] = Vector3.MoveTowards(vertices[2], vertices[13], Time.deltaTime * speed);
                vertices[3] = Vector3.MoveTowards(vertices[3], vertices[15], Time.deltaTime * speed + difDist);
                vertices[4] = Vector3.MoveTowards(vertices[4], vertices[10], Time.deltaTime * speed);
                vertices[5] = Vector3.MoveTowards(vertices[5], vertices[11], Time.deltaTime * speed + difDist);
                vertices[6] = Vector3.MoveTowards(vertices[6], vertices[12], Time.deltaTime * speed);
                vertices[7] = Vector3.MoveTowards(vertices[7], vertices[13], Time.deltaTime * speed);
                vertices[8] = Vector3.MoveTowards(vertices[8], vertices[14], Time.deltaTime * speed + difDist);
                vertices[9] = Vector3.MoveTowards(vertices[9], vertices[15], Time.deltaTime * speed + difDist);
            }
            else {
                vertices[0] = Vector3.MoveTowards(vertices[0], vertices[10], Time.deltaTime * speed);
                vertices[1] = Vector3.MoveTowards(vertices[1], vertices[11], Time.deltaTime * speed);
                vertices[2] = Vector3.MoveTowards(vertices[2], vertices[13], Time.deltaTime * speed);
                vertices[3] = Vector3.MoveTowards(vertices[3], vertices[15], Time.deltaTime * speed);
                vertices[4] = Vector3.MoveTowards(vertices[4], vertices[10], Time.deltaTime * speed);
                vertices[5] = Vector3.MoveTowards(vertices[5], vertices[11], Time.deltaTime * speed);
                vertices[6] = Vector3.MoveTowards(vertices[6], vertices[12], Time.deltaTime * speed);
                vertices[7] = Vector3.MoveTowards(vertices[7], vertices[13], Time.deltaTime * speed);
                vertices[8] = Vector3.MoveTowards(vertices[8], vertices[14], Time.deltaTime * speed);
                vertices[9] = Vector3.MoveTowards(vertices[9], vertices[15], Time.deltaTime * speed);
            }

            centers[0] = (vertices[0] + vertices[1] + vertices[2] + vertices[3]) * 0.25f;

            Vector3 tmpNorm = Vector3.Cross(vertices[2] - vertices[0], vertices[3] - vertices[0]).normalized;

            normals[0] = tmpNorm;
            normals[1] = tmpNorm;
            normals[2] = tmpNorm;
            normals[3] = tmpNorm;

            if (vertices[0] == vertices[10] && vertices[1] == vertices[11] && vertices[2] == vertices[13] && vertices[3] == vertices[15])
            {
                vertices.RemoveRange(4, 12);
                uvs.RemoveRange(4, 12);
                normals.RemoveRange(4, 12);
                triangles.RemoveRange(triangles.Count - 18, 18);
                if (delCent) { centers.RemoveAt(0); }
                delCent = !delCent;
                mesh.Clear();
                mesh.vertices = vertices.ToArray();
                mesh.normals = normals.ToArray();
                mesh.uv = uvs.ToArray();
                mesh.triangles = triangles.ToArray();
            }
            else
            {
                mesh.vertices = vertices.ToArray();
            }
        }
        else
        {
            mesh.vertices = vertices.ToArray();
        }
        /*for (int i = 0; i < ((uvs.Count - 4) / 12); i++) {
            if (i != 0) {
                uvs[4 + (i * 12)] = uvs[4 + ((i - 1) * 12) + 6];
                uvs[4 + (i * 12) + 1] = uvs[4 + ((i - 1) * 12) + 7];
            }
            float acDist = Vector3.Distance(
                (vertices[4 + (i * 12) + 6] + vertices[4 + (i * 12) + 7]) / 2,
                (vertices[4 + (i * 12)] + vertices[4 + (i * 12) + 1]) / 2
                );
            uvs[4 + (i * 12) + 6] = new Vector2(1, uvs[4 + (i * 12)].y + acDist);
            uvs[4 + (i * 12) + 7] = new Vector2(0, uvs[4 + (i * 12) + 1].y + acDist);
        }*/

        for (int i = ((uvs.Count - 4) / 12) - 1; i >= 0; i--)
        {
            if (i == ((uvs.Count - 4) / 12) - 1)
            {
                uvs[4 + (i * 12) + 6] = new Vector2(0, 0);
                uvs[4 + (i * 12) + 7] = new Vector2(1, 0);
            }
            else
            {
                uvs[4 + (i * 12) + 6] = uvs[4 + ((i + 1) * 12)];
                uvs[4 + (i * 12) + 7] = uvs[4 + ((i + 1) * 12) + 1];
            }
            float acDist = Vector3.Distance(
                (vertices[4 + (i * 12) + 6] + vertices[4 + (i * 12) + 7]) / 2,
                (vertices[4 + (i * 12)] + vertices[4 + (i * 12) + 1]) / 2
                );
            uvs[4 + (i * 12)] = new Vector2(0, uvs[4 + (i * 12) + 6].y + acDist);
            uvs[4 + (i * 12) + 1] = new Vector2(1, uvs[4 + (i * 12) + 7].y + acDist);
        }

        mesh.uv = uvs.ToArray();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private float getDistance()
    {
        float dist = 0f;

        for (int i = 0; i < centers.Count - 1; i++)
        {
            dist += Vector3.Distance(centers[i], centers[i + 1]);
        }
        return dist;
    }

    public void newPoints(int dir)
    {
        if (dir == 0)
        {
            centers[centers.Count - 1] = _pl.transform.position + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown;
            centers.Add(_pl.transform.position + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices[mesh.vertices.Length - 6] = _pl.transform.position + _pl.transform.right * size - _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown;
            vertices[mesh.vertices.Length - 5] = _pl.transform.position - _pl.transform.right * size - _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown;
            vertices[mesh.vertices.Length - 4] = _pl.transform.position + _pl.transform.right * size - _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown;
            vertices[mesh.vertices.Length - 3] = _pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown;
            vertices[mesh.vertices.Length - 2] = _pl.transform.position - _pl.transform.right * size - _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown;
            vertices[mesh.vertices.Length - 1] = _pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown;

            vertices.Add(_pl.transform.position + _pl.transform.right * size - _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size - _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position + _pl.transform.right * size - _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size - _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);

            vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);

            vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
            vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown + _pl.transform.forward * offsetDown);
        }
        else
        {
            centers[centers.Count - 1] = _pl.transform.position + _pl.transform.up * -offsetDown;
            centers.Add(_pl.transform.position + _pl.transform.up * -offsetDown);

            if (dir == 1)
            {
                vertices[mesh.vertices.Length - 6] = _pl.transform.position + _pl.transform.right * dir * .5f + _pl.transform.forward * dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 5] = _pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * -dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 4] = _pl.transform.position + _pl.transform.right * dir * .5f + _pl.transform.forward * dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 3] = _pl.transform.position + _pl.transform.right * dir * .5f + _pl.transform.forward * dir * size - _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 2] = _pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * -dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 1] = _pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * -dir * size - _pl.transform.up * size + _pl.transform.up * -offsetDown;

                vertices.Add(_pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * -dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * dir * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * -dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * -dir * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);

                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * 0.5f + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * 0.5f + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * 0.5f - _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);

                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * 0.5f + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * 0.5f + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * 0.5f - _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
            }
            else
            {
                vertices[mesh.vertices.Length - 6] = _pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 5] = _pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * -dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 4] = _pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 3] = _pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * dir * size - _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 2] = _pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * -dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown;
                vertices[mesh.vertices.Length - 1] = _pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * -dir * size - _pl.transform.up * size + _pl.transform.up * -offsetDown;


                vertices.Add(_pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * -dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * size + _pl.transform.forward * dir * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * -dir * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * dir * 0.5f + _pl.transform.forward * -dir * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);

                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * 0.5f + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * 0.5f + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * 0.5f - _pl.transform.up * size + _pl.transform.up * -offsetDown);

                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * 0.5f + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.forward * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * 0.5f + _pl.transform.up * size + _pl.transform.up * -offsetDown);
                vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.forward * 0.5f - _pl.transform.up * size + _pl.transform.up * -offsetDown);
            }
        }

        vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position + _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position + _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size + _pl.transform.up * size + _pl.transform.up * -offsetDown);
        vertices.Add(_pl.transform.position - _pl.transform.right * size - _pl.transform.up * size + _pl.transform.up * -offsetDown);

        if (dir == 0)
        {
            Vector3 tmpNorm = Vector3.Cross(vertices[mesh.vertices.Length] - vertices[mesh.vertices.Length + 6], vertices[mesh.vertices.Length + 1] - vertices[mesh.vertices.Length + 6]).normalized;

            normals.Add(tmpNorm);
            normals.Add(tmpNorm);
            normals.Add(_pl.transform.right);
            normals.Add(_pl.transform.right);
            normals.Add(-_pl.transform.right);
            normals.Add(-_pl.transform.right);

            normals.Add(tmpNorm);
            normals.Add(tmpNorm);
            normals.Add(_pl.transform.right);
            normals.Add(_pl.transform.right);
            normals.Add(-_pl.transform.right);
            normals.Add(-_pl.transform.right);
        }

        else
        {
            Vector3 tmpNormR = Vector3.Cross(vertices[mesh.vertices.Length + 2] - vertices[mesh.vertices.Length + 3], vertices[mesh.vertices.Length + 8] - vertices[mesh.vertices.Length + 3]).normalized;
            Vector3 tmpNormL = Vector3.Cross(vertices[mesh.vertices.Length + 10] - vertices[mesh.vertices.Length + 11], vertices[mesh.vertices.Length + 4] - vertices[mesh.vertices.Length + 11]).normalized;

            normals.Add(_pl.transform.up);
            normals.Add(_pl.transform.up);
            normals.Add(tmpNormR);
            normals.Add(tmpNormR);
            normals.Add(tmpNormL);
            normals.Add(tmpNormL);

            normals.Add(_pl.transform.up);
            normals.Add(_pl.transform.up);
            normals.Add(tmpNormR);
            normals.Add(tmpNormR);
            normals.Add(tmpNormL);
            normals.Add(tmpNormL);
        }

        normals.Add(_pl.transform.up);
        normals.Add(_pl.transform.up);
        normals.Add(_pl.transform.right);
        normals.Add(_pl.transform.right);
        normals.Add(-_pl.transform.right);
        normals.Add(-_pl.transform.right);

        normals.Add(_pl.transform.up);
        normals.Add(_pl.transform.up);
        normals.Add(_pl.transform.right);
        normals.Add(_pl.transform.right);
        normals.Add(-_pl.transform.right);
        normals.Add(-_pl.transform.right);

        triangles.Add(mesh.vertices.Length);
        triangles.Add(mesh.vertices.Length + 1);
        triangles.Add(mesh.vertices.Length + 6);
        triangles.Add(mesh.vertices.Length + 1);
        triangles.Add(mesh.vertices.Length + 7);
        triangles.Add(mesh.vertices.Length + 6);

        triangles.Add(mesh.vertices.Length + 8);
        triangles.Add(mesh.vertices.Length + 9);
        triangles.Add(mesh.vertices.Length + 3);
        triangles.Add(mesh.vertices.Length + 8);
        triangles.Add(mesh.vertices.Length + 3);
        triangles.Add(mesh.vertices.Length + 2);

        triangles.Add(mesh.vertices.Length + 4);
        triangles.Add(mesh.vertices.Length + 5);
        triangles.Add(mesh.vertices.Length + 11);
        triangles.Add(mesh.vertices.Length + 4);
        triangles.Add(mesh.vertices.Length + 11);
        triangles.Add(mesh.vertices.Length + 10);

        triangles.Add(mesh.vertices.Length + 12);
        triangles.Add(mesh.vertices.Length + 13);
        triangles.Add(mesh.vertices.Length + 18);
        triangles.Add(mesh.vertices.Length + 13);
        triangles.Add(mesh.vertices.Length + 19);
        triangles.Add(mesh.vertices.Length + 18);

        triangles.Add(mesh.vertices.Length + 20);
        triangles.Add(mesh.vertices.Length + 21);
        triangles.Add(mesh.vertices.Length + 15);
        triangles.Add(mesh.vertices.Length + 20);
        triangles.Add(mesh.vertices.Length + 15);
        triangles.Add(mesh.vertices.Length + 14);

        triangles.Add(mesh.vertices.Length + 16);
        triangles.Add(mesh.vertices.Length + 17);
        triangles.Add(mesh.vertices.Length + 23);
        triangles.Add(mesh.vertices.Length + 16);
        triangles.Add(mesh.vertices.Length + 23);
        triangles.Add(mesh.vertices.Length + 22);

        for (int i = 0; i < 24; i++)
        {
            uvs.Add(new Vector2(0, 1));
        }

        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
    }
}
