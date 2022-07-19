using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Arch_type5_perimeter : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    const float PI = 3.1415926535897931f;


    const int N = 6;

    public float c1 = 1.25f;//x-direction
    public float c2 = 1.5f;//y-direction
    public float c3 = 0.875f;//z-direction

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

    public static Vector3 Arch_eq_6(float x, float z, float c1, float c2)
    {
        float y;
        y = c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (c1 + 0.00001f));
        y = y + c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (c1 * c1 + 0.00001f));
        y = y / 2.0f;
        return new Vector3(x, y, z);
    }

    public static float uv_Arch_eq_6(float x, float z, float c1, float c2)
    {
        float y;
        y =  Mathf.Sqrt(1 - Mathf.Abs(x) / (1.0f + 0.00001f));
        y = y + Mathf.Sqrt(1 - Mathf.Abs((x) * (x)) / (1.0f + 0.00001f));
        y = y / 2.0f - 0.00001f;
        return y;
    }

    void MakeDiscreteProceduralGrid()
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;


        Mesh m = new Mesh();

        vertices = new Vector3[2 * (2 * N + 1 + 2) ];
        triangles = new int[3 * 4 * N];

        uvs = new Vector2[2 * (2 * N + 1 + 2)];

        float x;
        float z;

        for (int i = 0; i < 2 * N + 1; i++)//1
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i] = Arch_eq_6(x, z, c1, c2);
        }
        vertices[2 * N + 1] = new Vector3(-c1,c2,0.0f);
        vertices[2 * N + 2] = new Vector3(c1, c2, 0.0f);
        for (int i = 0; i < 2 * N + 1; i++)//1
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = c3;
            vertices[2 * N + 3 + i] = Arch_eq_6(x, z, c1, c2);
        }
        vertices[4 * N + 4] = new Vector3(-c1, c2, c3);
        vertices[4 * N + 5] = new Vector3(c1, c2, c3);

        for (int i = 0; i < N; i++)
        {
            triangles[3 * i + 0] = i + 0;
            triangles[3 * i + 1] = 2 * N + 1;
            triangles[3 * i + 2] = i + 1;
        }
        for (int i = N; i < 2 * N; i++)
        {
            triangles[3 * i + 0] = i + 0;
            triangles[3 * i + 1] = 2 * N + 2;
            triangles[3 * i + 2] = i + 1;
        }
        for (int i = 2 * N + 3; i < 3 * N + 3; i++)
        {
            triangles[3 * (i - 3) + 0] = 4 * N + 4;
            triangles[3 * (i - 3) + 1] = i + 0;
            triangles[3 * (i - 3) + 2] = i + 1;
        }
        for (int i = 3 * N + 3; i < 4 * N + 3; i++)
        {
            triangles[3 * (i - 3) + 0] = 4 * N + 5;
            triangles[3 * (i - 3) + 1] = i + 0;
            triangles[3 * (i - 3) + 2] = i + 1;
        }

        float xx;
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = 0.5f + 0.5f * Mathf.Cos(PI - i * delta_theta);
            xx = Mathf.Cos(PI - i * delta_theta);
            uvs[i] = new Vector2(x, uv_Arch_eq_6(xx, 0.0f, c1, c2));
        }
        uvs[2 * N + 1] = new Vector2(0.0f, 1.0f);
        uvs[2 * N + 2] = new Vector2(1.0f, 1.0f);


        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = 0.5f + 0.5f * Mathf.Cos(PI - i * delta_theta);
            xx = Mathf.Cos(PI - i * delta_theta);
            uvs[2 * N + 3 + i] = new Vector2(x, uv_Arch_eq_6(xx, 0.0f, c1, c2));
        }
        uvs[4 * N + 4] = new Vector2(0.0f, 1.0f);
        uvs[4 * N + 5] = new Vector2(1.0f, 1.0f);



    }






void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }


}
