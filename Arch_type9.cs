using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Arch_type9 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    const float PI = 3.1415926535897931f;


    const int N = 5;

    public float c1 = 1.0f;//x-direction
    public float c2 = 1.25f;//y-direction
    public float c3 = 0.25f;//z-direction
    public float c4 = -1.25f;//Length of Sides
    public float p = 0.125f;


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
    public static Vector3 Arch_eq(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.8f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (0.8f * c1 + 0.00001f));
        y = y + 0.8f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (0.8f * c1 * 0.8f * c1 + 0.00001f));
        y = y / 2.0f;

        return new Vector3(x, y, z);
    }
    public static Vector3 Arch_eq_2(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.9f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (0.9f * c1 + 0.00001f));
        y = y + 0.9f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (0.9f * c1 * 0.9f * c1 + 0.00001f));
        y = y / 2.0f;

        return new Vector3(x, y, z);
    }

    public static Vector3 Arch_eq_3(float x, float z, float c1, float c2)
    {
        float y;
        y = c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (c1 + 0.00001f));
        y = y + c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / ((c1) * (c1) + 0.00001f));
        y = y / 2.0f;
        return new Vector3(x, y, z);
    }


    void MakeDiscreteProceduralGrid()
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        Mesh m = new Mesh();

        vertices = new Vector3[(2 * N + 1) * 6 + 3];
        triangles = new int[8 * 6 * N + 4 * 3];
        uvs = new Vector2[(2 * N + 1) * 6 + 3];

        float x;
        float z;

        float f1 = 0.9f;
        float f2 = 0.8f;
        float f3 = (f1 - f2);

        //left arch
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i] = Arch_eq_3(x, z, c1, c2) - new Vector3(c1*(f2+f3), 0, 0);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f1 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = p;
            vertices[i + (2 * N + 1)] = Arch_eq_2(x, z, c1, c2) - new Vector3(c1 * (f2 + f3), 0, 0);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f2 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i + 2 * (2 * N + 1)] = Arch_eq(x, z, c1, c2) - new Vector3(c1 * (f2 + f3), 0, 0);
        }        
        //right Arch
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i + 3 * (2 * N + 1)] = Arch_eq_3(x, z, c1, c2) + new Vector3(c1 * (f2 + f3), 0, 0);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f1 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = p;
            vertices[i + 4 * (2 * N + 1)] = Arch_eq_2(x, z, c1, c2) + new Vector3(c1 * (f2 + f3), 0, 0);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f2 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i + 5 * (2 * N + 1)] = Arch_eq(x, z, c1, c2) + new Vector3(c1 * (f2 + f3), 0,0);
        }


        //bottom part:
        int j = 2 * N;
        x = c1;
        z = 0.0f;
        vertices[j + 5 * (2 * N + 1) + 1] = new Vector3(x - c1 * (f2 + f3), c4, 0);

        x = f1 * c1;
        z = p;
        vertices[j + 5 * (2 * N + 1) + 2] = new Vector3(x - c1 * (f2 + f3), c4, z);

        x = f2 * c1;
        z = 0.0f;
        vertices[j + 5 * (2 * N + 1) + 3] = new Vector3(x - c1 * (f2 + f3), c4, 0);







        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * i + 0] = i + 0;
            triangles[6 * i + 1] = i + (2 * N + 1);
            triangles[6 * i + 2] = i + 1;
            triangles[6 * i + 3] = i + (2 * N + 1);
            triangles[6 * i + 4] = i + (2 * N + 1) + 1;
            triangles[6 * i + 5] = i + 1;
        }
        
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 2 * N + 6 * i + 0] = (2 * N + 1) + i + 0;
            triangles[6 * 2 * N + 6 * i + 1] = (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 2 * N + 6 * i + 2] = (2 * N + 1) + i + 1;
            triangles[6 * 2 * N + 6 * i + 3] = (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 2 * N + 6 * i + 4] = (2 * N + 1) + i + (2 * N + 1) + 1;
            triangles[6 * 2 * N + 6 * i + 5] = (2 * N + 1) + i + 1;
        }
        
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 4 * N + 6 * i + 0] = 3 * (2 * N + 1) + i + 0;
            triangles[6 * 4 * N + 6 * i + 1] = 3 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 4 * N + 6 * i + 2] = 3 * (2 * N + 1) + i + 1;
            triangles[6 * 4 * N + 6 * i + 3] = 3 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 4 * N + 6 * i + 4] = 3 * (2 * N + 1) + i + (2 * N + 1) + 1;
            triangles[6 * 4 * N + 6 * i + 5] = 3 * (2 * N + 1) + i + 1;
        }
        
        
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 6 * N + 6 * i + 0] = 4 * (2 * N + 1) + i + 0;
            triangles[6 * 6 * N + 6 * i + 1] = 4 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 6 * N + 6 * i + 2] = 4 * (2 * N + 1) + i + 1;
            triangles[6 * 6 * N + 6 * i + 3] = 4 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 6 * N + 6 * i + 4] = 4 * (2 * N + 1) + i + (2 * N + 1) + 1;
            triangles[6 * 6 * N + 6 * i + 5] = 4 * (2 * N + 1) + i + 1;
        }


        int bp_index = 6 * 6 * N + 6 * (2 * N - 1) + 5;
        int bp_vert = 2 * N + 5 * (2 * N + 1) + 1;
        
        triangles[bp_index + 1] = 2 * N;
        triangles[bp_index + 2] = 2 * N + (2 * N + 1);
        triangles[bp_index + 3] = bp_vert;

        triangles[bp_index + 4] = 2 * N + (2 * N + 1);
        triangles[bp_index + 5] = bp_vert + 1;
        triangles[bp_index + 6] = bp_vert;

        triangles[bp_index + 7] = 2 * N + (2 * N + 1);
        triangles[bp_index + 8] = 2 * N + (2 * N + 1) * 2;
        triangles[bp_index + 9] = bp_vert + 1;

        triangles[bp_index + 10] = 2 * N + 2 * (2 * N + 1);
        triangles[bp_index + 11] = bp_vert + 2;
        triangles[bp_index + 12] = bp_vert + 1;

        float offset1 = 0.0f;
        float Scale = 0.5f;
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i] = new Vector2(offset1 + Scale * i / (2.00001f * N), 0.0f);
            uvs[i + 2 * N + 1] = new Vector2(offset1 + Scale * i / (2.00001f * N), 1.0f / 2.0f);
            uvs[i + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 0.0f);
            uvs[i + 2 * N + 1 + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 1.0f / 2.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 2 * N + 1] = new Vector2(offset1 + Scale * i / (2.00001f * N), 1.0f / 2.0f);
            uvs[i + 2 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 2.0f / 2.0f);
            uvs[i + 2 * N + 1 + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 1.0f / 2.0f);
            uvs[i + 2 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 2.0f / 2.0f);
        }

        
        offset1 = 0.5f;
        Scale = 0.5f;
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 3 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 0.0f / 2.0f);
            uvs[i + 4 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 1.0f / 2.0f);
            uvs[i + 3 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 0.0f / 2.0f);
            uvs[i + 4 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 1.0f / 2.0f);
        }
        
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 4 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 3.0f / 6.0f);
            uvs[i + 5 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 4.0f / 6.0f);
            uvs[i + 4 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 3.0f / 6.0f);
            uvs[i + 5 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 4.0f / 6.0f);
        }

        int texture_index = 2 * N - 1 + 5 * (2 * N + 1) + 1;

        uvs[texture_index + 1] = new Vector2(0.0f, 0.0f);
        uvs[texture_index + 2] = new Vector2(0.0f, 1.0f / 2.0001f);
        uvs[texture_index + 3] = new Vector2(0.0f, 2.0f / 2.0001f);

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