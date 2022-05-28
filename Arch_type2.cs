using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]

public class Arch_type2 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    const float PI = 3.1415926535897931f;


    const int N = 4;

    const float c1 = 4.25f;//x-direction
    const float c2 = 2.75f;//y-direction
    const float c3 = 3.75f;//z-direction


    //    const float c1 = 0.90625f;//x-direction
    //    const float c2 = 1.125f;//y-direction
    //    const float c3 = 0.25f;//z-direction

    float delta_theta = (PI / 2) / N;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    void Start()
    {
        MakeDiscreteProceduralGrid();
        UpdateMesh();
    }

    public static Vector3 Arch_eq(float x, float z, float c1, float c2, float c3)
    {
        float y;
        if (Mathf.Abs(x / (c1 + 0.000001f)) < Mathf.Abs(z/(c3 + 0.000001f)))
        {
//            if (Mathf.Abs(z)<Mathf.Abs(x)) {
            y = c2 * (1.0f - 0.5f * Mathf.Abs(x / (c1 + 0.000001f)));
            y = y + c2 * Mathf.Sqrt(1 - (x * x) / (c1 * 1.1547006f * c1 * 1.1547006f + 0.000001f));
            y = y / 2.0f - 0.5f * c2;
        }
        else {
            y = c2 * (1.0f - 0.5f * Mathf.Abs(z / (c3 + 0.000001f)));
            y = y + c2 * Mathf.Sqrt(1 - (z * z) / (c3 * 1.1547006f * c3 * 1.1547006f + 0.000001f));
            y = y / 2.0f - 0.5f * c2;
        }
        return new Vector3(x, y, z);
    }

    void MakeDiscreteProceduralGrid()
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh m = new Mesh();

        //N=number of splits per quarter.

        vertices = new Vector3[(2 * N + 1)*(2 * N + 1)];
        triangles = new int[24 * N * N];
        uvs = new Vector2[(2 * N + 1) * (2 * N + 1)];

        float x;
        float z;

        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = -1.0f * c1 + 2.0f * c1 * i / (2.0f * N);
            for (int j = 0; j < 2 * N + 1; j++)
            {
                z = -1.0f * c3 + 2.0f * j * c3 / (2.0f * N);
                vertices[j + i * (2 * N + 1)] = Arch_eq(x, z, c1, c2, c3);
            }
        }


        int l = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                triangles[6 * l + 0] = j + 0 + (2 * N + 1) * i;
                triangles[6 * l + 1] = j + (2 * N + 1) * (i + 1) + 1;
                triangles[6 * l + 2] = j + 1 + (2 * N + 1) * i;
                triangles[6 * l + 3] = j + (2 * N + 1) * (i + 1);
                triangles[6 * l + 4] = j + (2 * N + 1) * (i + 1) + 1;
                triangles[6 * l + 5] = j + 0 + (2 * N + 1) * i;
                l++;
            }
        }
        
        for (int i = 0; i < N; i++)
        {
            for (int j = N; j < 2 * N; j++)
            {
                triangles[6 * l + 0] = j + 0 + (2 * N + 1) * i;
                triangles[6 * l + 1] = j + (2 * N + 1) * (i + 1);
                triangles[6 * l + 2] = j + 1 + (2 * N + 1) * i;
                triangles[6 * l + 3] = j + (2 * N + 1) * (i + 1);
                triangles[6 * l + 4] = j + (2 * N + 1) * (i + 1) + 1;
                triangles[6 * l + 5] = j + 1 + (2 * N + 1) * i;
                l++;
            }
        }

        for (int i = N; i < 2 * N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                triangles[6 * l + 0] = j + 0 + (2 * N + 1) * i;
                triangles[6 * l + 1] = j + (2 * N + 1) * (i + 1);
                triangles[6 * l + 2] = j + 1 + (2 * N + 1) * i;
                triangles[6 * l + 3] = j + (2 * N + 1) * (i + 1);
                triangles[6 * l + 4] = j + (2 * N + 1) * (i + 1) + 1;
                triangles[6 * l + 5] = j + 1 + (2 * N + 1) * i;
                l++;
            }
        }
        for (int i = N; i < 2 * N  ; i++)
        {
            for (int j = N; j < 2 * N; j++)
            {
                triangles[6 * l + 0] = j + 0 + (2 * N + 1) * i;
                triangles[6 * l + 1] = j + (2 * N + 1) * (i + 1) + 1;
                triangles[6 * l + 2] = j + 1 + (2 * N + 1) * i;
                triangles[6 * l + 3] = j + (2 * N + 1) * (i + 1) + 1;
                triangles[6 * l + 4] = j + 0 + (2 * N + 1) * i;
                triangles[6 * l + 5] = j + (2 * N + 1) * (i + 1);
                l++;
            }
        }
        
        
        for (int j = 0; j < 2 * N; j++)
        {
            for (int i = 0; i < 2 * N; i++)
            {
                uvs[i + (2 * N + 1) * j] = new Vector2(i / (2.00001f * N), j / (2.00001f * N));
                uvs[i + (2 * N + 1) * j + 1] = new Vector2((i + 1) / (2.00001f * N), j / (2.00001f * N));
                uvs[i + (2 * N + 1) * (j + 1)] = new Vector2(i / (2.00001f * N), (j + 1) / (2.00001f * N));
                uvs[i + (2 * N + 1) * (j + 1) + 1] = new Vector2((i + 1) / (2.00001f * N), (j + 1) / (2.00001f * N));
            }
        }
        
    }

    void UpdateMesh()
    {
        mesh.Clear();


        //        mr.material = Ring_mat;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //        mesh.RecalculateBounds();
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

}
