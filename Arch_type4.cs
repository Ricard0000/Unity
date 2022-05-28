using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Arch_type4 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    const float PI = 3.1415926535897931f;


    const int N = 6;

    const float c1 = 1.25f;//x-direction
    const float c2 = 1.5f;//y-direction
    const float c3 = 0.875f;//z-direction


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
        y = 0.75f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (0.75f * c1 + 0.00001f));
        y = y + 0.75f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (0.75f * c1 * 0.75f * c1 + 0.00001f));
        y = y / 2.0f;

        return new Vector3(x, y, z);
    }
    public static Vector3 Arch_eq_2(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.8f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (0.8f * c1 + 0.00001f));
        y = y + 0.8f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (0.8f * c1 * 0.8f * c1 + 0.00001f));
        y = y / 2.0f;

        return new Vector3(x, y, z);
    }

    public static Vector3 Arch_eq_3(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.85f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (c1 * 0.85f + 0.00001f));
        y = y + 0.85f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / ((c1 * 0.85f) * (c1 * 0.85f) + 0.00001f));
        y = y / 2.0f;
        return new Vector3(x, y, z);
    }


    public static Vector3 Arch_eq_4(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.9f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (0.9f * c1 + 0.00001f));
        y = y + 0.9f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (0.9f * c1 * 0.9f * c1 + 0.00001f));
        y = y / 2.0f;
        return new Vector3(x, y, z);
    }

    public static Vector3 Arch_eq_5(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.95f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (0.95f * c1 + 0.00001f));
        y = y + 0.95f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (0.95f * c1 * 0.95f * c1 + 0.00001f));
        y = y / 2.0f;
        return new Vector3(x, y, z);
    }
    public static Vector3 Arch_eq_6(float x, float z, float c1, float c2)
    {
        float y;
        y = c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (c1 + 0.00001f));
        y = y + c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (c1 * c1 + 0.00001f));
        y = y / 2.0f;
        return new Vector3(x, y, z);
    }

    void MakeDiscreteProceduralGrid()
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;


        //Should be based on the equations:
        //x=-c2((y/c1)^2-1)   for  x>0
        //x=-x for x<0

        Mesh m = new Mesh();

        //        N=Grid size in the y-direction
        //        N+1 in the y-direction
        //        2(N+1) in the x-direction
        //        2(N + 1) in the z-direction


        vertices = new Vector3[2 * (2 * N + 1) * 8];
        triangles = new int[3 * 4 * N * 15]; //3 * 4 * N * 7

        uvs = new Vector2[2 * (2 * N + 1) * 8];

        float x;
        float z;

        float f1 = 0.75f;
        float f2 = 0.80f;
        float f3 = 0.85f;
        float f4 = 0.90f;
        float f5 = 0.95f;


        for (int i = 0; i < 2 * N + 1; i++)//1
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i] = Arch_eq_6(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//2
        {
            x = f5 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i + (2 * N + 1)] = Arch_eq_5(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//3
        {
            x = f4 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 1.0f/9.0f;
            vertices[i + 2 * (2 * N + 1)] = Arch_eq_4(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//4
        {
            x = f4 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 2.0f * c3 / 9.0f;
            vertices[i + 3 * (2 * N + 1)] = Arch_eq_4(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//5
        {
            x = f3 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 2.0f * c3 / 9.0f;
            vertices[i + 4 * (2 * N + 1)] = Arch_eq_3(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//6
        {
            x = f3 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 3.0f * c3 / 9.0f;
            vertices[i + 5 * (2 * N + 1)] = Arch_eq_3(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//7
        {
            x = f2 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 3.0f * c3 / 9.0f;
            vertices[i + 6 * (2 * N + 1)] = Arch_eq_2(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//8
        {
            x = f1 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 4.0f * c3 / 9.0f;
            vertices[i + 7 * (2 * N + 1)] = Arch_eq(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//9
        {
            x = f1 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 5.0f * c3 / 9.0f;
            vertices[i + 8 * (2 * N + 1)] = Arch_eq(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//10
        {
            x = f2 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 6.0f * c3 / 9.0f;
            vertices[i + 9 * (2 * N + 1)] = Arch_eq_2(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//11
        {
            x = f3 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 6.0f * c3 / 9.0f;
            vertices[i + 10 * (2 * N + 1)] = Arch_eq_3(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//12
        {
            x = f3 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 7.0f * c3 / 9.0f;
            vertices[i + 11 * (2 * N + 1)] = Arch_eq_3(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//13
        {
            x = f4 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 7.0f * c3 / 9.0f;
            vertices[i + 12 * (2 * N + 1)] = Arch_eq_4(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//14
        {
            x = f4 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 8.0f * c3 / 9.0f;
            vertices[i + 13 * (2 * N + 1)] = Arch_eq_4(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//15
        {
            x = f5 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 9.0f * c3 / 9.0f;
            vertices[i + 14 * (2 * N + 1)] = Arch_eq_5(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)//16
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = 9.0f * c3 / 9.0f;
            vertices[i + 15 * (2 * N + 1)] = Arch_eq_6(x, z, c1, c2);
        }


        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * i + 0] = i + 0;
            triangles[6 * i + 1] = i + 1;
            triangles[6 * i + 2] = i + (2 * N + 1);
            triangles[6 * i + 3] = i + (2 * N + 1);
            triangles[6 * i + 4] = i + 1;
            triangles[6 * i + 5] = i + (2 * N + 1) + 1;
        }

        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 2 * N + 6 * i + 0] = (2 * N + 1) + i + 0;
            triangles[6 * 2 * N + 6 * i + 1] = (2 * N + 1) + i + 1;
            triangles[6 * 2 * N + 6 * i + 2] = (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 2 * N + 6 * i + 3] = (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 2 * N + 6 * i + 4] = (2 * N + 1) + i + 1;
            triangles[6 * 2 * N + 6 * i + 5] = (2 * N + 1) + i + (2 * N + 1) + 1;
        }

        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 4 * N + 6 * i + 0] = 2 * (2 * N + 1) + i + 0;
            triangles[6 * 4 * N + 6 * i + 1] = 2 * (2 * N + 1) + i + 1;
            triangles[6 * 4 * N + 6 * i + 2] = 2 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 4 * N + 6 * i + 3] = 2 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 4 * N + 6 * i + 4] = 2 * (2 * N + 1) + i + 1;
            triangles[6 * 4 * N + 6 * i + 5] = 2 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }

        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 6 * N + 6 * i + 0] = 3 * (2 * N + 1) + i + 0;
            triangles[6 * 6 * N + 6 * i + 1] = 3 * (2 * N + 1) + i + 1;
            triangles[6 * 6 * N + 6 * i + 2] = 3 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 6 * N + 6 * i + 3] = 3 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 6 * N + 6 * i + 4] = 3 * (2 * N + 1) + i + 1;
            triangles[6 * 6 * N + 6 * i + 5] = 3 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }

        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 8 * N + 6 * i + 0] = 4 * (2 * N + 1) + i + 0;
            triangles[6 * 8 * N + 6 * i + 1] = 4 * (2 * N + 1) + i + 1;
            triangles[6 * 8 * N + 6 * i + 2] = 4 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 8 * N + 6 * i + 3] = 4 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 8 * N + 6 * i + 4] = 4 * (2 * N + 1) + i + 1;
            triangles[6 * 8 * N + 6 * i + 5] = 4 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 10 * N + 6 * i + 0] = 5 * (2 * N + 1) + i + 0;
            triangles[6 * 10 * N + 6 * i + 1] = 5 * (2 * N + 1) + i + 1;
            triangles[6 * 10 * N + 6 * i + 2] = 5 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 10 * N + 6 * i + 3] = 5 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 10 * N + 6 * i + 4] = 5 * (2 * N + 1) + i + 1;
            triangles[6 * 10 * N + 6 * i + 5] = 5 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 12 * N + 6 * i + 0] = 6 * (2 * N + 1) + i + 0;
            triangles[6 * 12 * N + 6 * i + 1] = 6 * (2 * N + 1) + i + 1;
            triangles[6 * 12 * N + 6 * i + 2] = 6 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 12 * N + 6 * i + 3] = 6 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 12 * N + 6 * i + 4] = 6 * (2 * N + 1) + i + 1;
            triangles[6 * 12 * N + 6 * i + 5] = 6 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 14 * N + 6 * i + 0] = 7 * (2 * N + 1) + i + 0;
            triangles[6 * 14 * N + 6 * i + 1] = 7 * (2 * N + 1) + i + 1;
            triangles[6 * 14 * N + 6 * i + 2] = 7 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 14 * N + 6 * i + 3] = 7 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 14 * N + 6 * i + 4] = 7 * (2 * N + 1) + i + 1;
            triangles[6 * 14 * N + 6 * i + 5] = 7 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 16 * N + 6 * i + 0] = 8 * (2 * N + 1) + i + 0;
            triangles[6 * 16 * N + 6 * i + 1] = 8 * (2 * N + 1) + i + 1;
            triangles[6 * 16 * N + 6 * i + 2] = 8 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 16 * N + 6 * i + 3] = 8 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 16 * N + 6 * i + 4] = 8 * (2 * N + 1) + i + 1;
            triangles[6 * 16 * N + 6 * i + 5] = 8 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 18 * N + 6 * i + 0] = 9 * (2 * N + 1) + i + 0;
            triangles[6 * 18 * N + 6 * i + 1] = 9 * (2 * N + 1) + i + 1;
            triangles[6 * 18 * N + 6 * i + 2] = 9 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 18 * N + 6 * i + 3] = 9 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 18 * N + 6 * i + 4] = 9 * (2 * N + 1) + i + 1;
            triangles[6 * 18 * N + 6 * i + 5] = 9 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 20 * N + 6 * i + 0] = 10 * (2 * N + 1) + i + 0;
            triangles[6 * 20 * N + 6 * i + 1] = 10 * (2 * N + 1) + i + 1;
            triangles[6 * 20 * N + 6 * i + 2] = 10 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 20 * N + 6 * i + 3] = 10 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 20 * N + 6 * i + 4] = 10 * (2 * N + 1) + i + 1;
            triangles[6 * 20 * N + 6 * i + 5] = 10 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 22 * N + 6 * i + 0] = 11 * (2 * N + 1) + i + 0;
            triangles[6 * 22 * N + 6 * i + 1] = 11 * (2 * N + 1) + i + 1;
            triangles[6 * 22 * N + 6 * i + 2] = 11 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 22 * N + 6 * i + 3] = 11 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 22 * N + 6 * i + 4] = 11 * (2 * N + 1) + i + 1;
            triangles[6 * 22 * N + 6 * i + 5] = 11 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 24 * N + 6 * i + 0] = 12 * (2 * N + 1) + i + 0;
            triangles[6 * 24 * N + 6 * i + 1] = 12 * (2 * N + 1) + i + 1;
            triangles[6 * 24 * N + 6 * i + 2] = 12 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 24 * N + 6 * i + 3] = 12 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 24 * N + 6 * i + 4] = 12 * (2 * N + 1) + i + 1;
            triangles[6 * 24 * N + 6 * i + 5] = 12 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 26 * N + 6 * i + 0] = 13 * (2 * N + 1) + i + 0;
            triangles[6 * 26 * N + 6 * i + 1] = 13 * (2 * N + 1) + i + 1;
            triangles[6 * 26 * N + 6 * i + 2] = 13 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 26 * N + 6 * i + 3] = 13 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 26 * N + 6 * i + 4] = 13 * (2 * N + 1) + i + 1;
            triangles[6 * 26 * N + 6 * i + 5] = 13 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 28 * N + 6 * i + 0] = 14 * (2 * N + 1) + i + 0;
            triangles[6 * 28 * N + 6 * i + 1] = 14 * (2 * N + 1) + i + 1;
            triangles[6 * 28 * N + 6 * i + 2] = 14 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 28 * N + 6 * i + 3] = 14 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 28 * N + 6 * i + 4] = 14 * (2 * N + 1) + i + 1;
            triangles[6 * 28 * N + 6 * i + 5] = 14 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }




        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i] = new Vector2(i / (2.00001f * N), 0.0f);
            uvs[i + 2 * N + 1] = new Vector2(i / (2.00001f * N), 1.0f / 15.0f);
            uvs[i + 1] = new Vector2((i + 1) / (2.00001f * N), 0.0f);
            uvs[i + 2 * N + 1 + 1] = new Vector2((i + 1) / (2.00001f * N), 1.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 2 * N + 1] = new Vector2(i / (2.00001f * N), 1.0f / 15.0f);
            uvs[i + 2 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 2.0f / 15.0f);
            uvs[i + 2 * N + 1 + 1] = new Vector2((i + 1) / (2.00001f * N), 1.0f / 15.0f);
            uvs[i + 2 * (2 * N + 1) + 1] = new Vector2((i + 1) / (2.00001f * N), 2.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 2 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 2.0f / 15.0f);
            uvs[i + 3 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 3.0f / 15.0f);
            uvs[i + 2 * (2 * N + 1) + 1] = new Vector2((i + 1) / (2.00001f * N), 2.0f / 15.0f);
            uvs[i + 3 * (2 * N + 1) + 1] = new Vector2((i + 1) / (2.00001f * N), 3.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 3 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 3.0f / 15.0f);
            uvs[i + 4 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 4.0f / 15.0f);
            uvs[i + 3 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 3.0f / 15.0f);
            uvs[i + 4 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 4.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 4 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 4.0f / 15.0f);
            uvs[i + 5 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 5.0f / 15.0f);
            uvs[i + 4 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 4.0f / 15.0f);
            uvs[i + 5 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 5.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 5 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 5.0f / 15.0f);
            uvs[i + 6 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 6.0f / 15.0f);
            uvs[i + 5 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 5.0f / 15.0f);
            uvs[i + 6 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 6.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 6 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 6.0f / 15.0f);
            uvs[i + 7 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 7.0f / 15.0f);
            uvs[i + 6 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 6.0f / 15.0f);
            uvs[i + 7 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 7.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 7 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 7.0f / 15.0f);
            uvs[i + 8 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 8.0f / 15.0f);
            uvs[i + 7 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 7.0f / 15.0f);
            uvs[i + 8 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 8.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 8 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 8.0f / 15.0f);
            uvs[i + 9 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 9.0f / 15.0f);
            uvs[i + 8 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 8.0f / 15.0f);
            uvs[i + 9 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 9.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 9 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 9.0f / 15.0f);
            uvs[i + 10 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 10.0f / 15.0f);
            uvs[i + 9 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 9.0f / 15.0f);
            uvs[i + 10 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 10.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 10 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 10.0f / 15.0f);
            uvs[i + 11 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 11.0f / 15.0f);
            uvs[i + 10 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 10.0f / 15.0f);
            uvs[i + 11 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 11.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 11 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 11.0f / 15.0f);
            uvs[i + 12 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 12.0f / 15.0f);
            uvs[i + 11 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 11.0f / 15.0f);
            uvs[i + 12 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 12.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 12 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 12.0f / 15.0f);
            uvs[i + 13 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 13.0f / 15.0f);
            uvs[i + 12 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 12.0f / 15.0f);
            uvs[i + 13 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 13.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 13 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 13.0f / 15.0f);
            uvs[i + 14 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 14.0f / 15.0f);
            uvs[i + 13 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 13.0f / 15.0f);
            uvs[i + 14 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 14.0f / 15.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 14 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 14.0f / 15.0f);
            uvs[i + 15 * (2 * N + 1)] = new Vector2(i / (2.00001f * N), 15.0f / 15.0f);
            uvs[i + 14 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 14.0f / 15.0f);
            uvs[i + 15 * (2 * N + 1) + 1] = new Vector2((i + 1.0f) / (2.00001f * N), 15.0f / 15.0f);
        }




        /*
        for (int i = 0; i < 2 * N + 1; i++)
        {
            uvs[2 * N + 1+i] = new Vector2(0.5f + 0.5f * Mathf.Cos(i * delta_theta - PI), 1.0f);
        }
        */
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